using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Infrastructure.Repositories;
using RetailOrdering.Infrastructure.Interfaces;
using RetailOrdering.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using RetailOrdering.API.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).Enrich.FromLogContext().CreateLogger();
builder.Host.UseSerilog();

// Database Connection Handling configured explicitly for SQLEXPRESS
var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(local)\\SQLEXPRESS;Database=retail_ordering_db;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )));

// System Component Registrations
builder.Services.AddAutoMapper(typeof(RetailOrdering.Application.Mapping.AutoMapperProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers().AddFluentValidation();

// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<RetailOrdering.Application.Validators.Auth.RegisterValidator>();

// ============================================================================
// ANGULAR CORS CONFIGURATION
// ============================================================================
var myAngularPolicy = "_myAngularPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAngularPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular default port
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Required if you use cookies/identity sessions
        });
});

builder.Services.AddHealthChecks();

// ============================================================================
// IDENTITY / JWT SECURITY CONFIGURATION (UPDATED TO STANDARD SCHEMES)
// ============================================================================
var jwtKey = configuration["Jwt:Key"] ?? "ChangeThis_SecretKey1234567890_ExtraLengthFor256Bits";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    // FIX: Using standard defaults so it registers properly as "Bearer"
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => // FIX: Removed custom string name to resolve the 401 controller routing bypass
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        RoleClaimType = System.Security.Claims.ClaimTypes.Role
    };
});

// Register application services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddAuthorization();

// ============================================================================
// OPENAPI/SWAGGER CONFIGURATION (FIXED SECURITY REQUIREMENT OBJECT REFERENCE)
// ============================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RetailOrdering API", Version = "v1" });

    // Define the HTTP Bearer setup
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter ONLY your raw JWT token. Do not include the word 'Bearer'.",
    });

    // FIX: Map the security requirement using an explicit reference metadata link
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Must exactly match the string used in AddSecurityDefinition
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RetailOrdering API v1"));
}

app.UseHttpsRedirection();

// ============================================================================
// MIDDLEWARE PIPELINE EXECUTION ORDER
// ============================================================================
app.UseRouting();
app.UseCors(myAngularPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapGet("/", () => new { success = true, message = "API running" });
app.MapControllers();

app.Run();
