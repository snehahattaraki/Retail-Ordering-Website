using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailOrdering.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDefaultCors();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseApiMiddlewares();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
