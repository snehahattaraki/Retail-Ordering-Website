using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RetailOrdering.Infrastructure.Data
{
    // FIXED: Changed IDesignTimeDesignTimeDbContextFactory to IDesignTimeDbContextFactory
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var conn = config.GetConnectionString("DefaultConnection")
                ?? "Server=(local)\\SQLEXPRESS;Database=retail_ordering_db;Trusted_Connection=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Target Microsoft SQL Server for Entity Framework Scaffolding
            optionsBuilder.UseSqlServer(conn, sqlOptions => sqlOptions.EnableRetryOnFailure());

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}