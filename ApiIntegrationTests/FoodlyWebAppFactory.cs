using Foodly.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

internal class FoodlyWebApplicationFactory : WebApplicationFactory<Program>
{
    override protected void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove the existing DbContextOptions
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

            // Register a new DBContext that will use our test connection string
            string? connString = GetConnectionString();
            services.AddSqlite<AppDbContext>(connString);

            // Delete the database (if exists) to ensure we start clean
            AppDbContext dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        });
    }

    private static string? GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<FoodlyWebApplicationFactory>()
            .Build();

        //Kom ihåg att köra dessa kommandon för att sätta upp user-secrets!
        //dotnet user-secrets init
        //dotnet user-secrets set "ConnectionStrings:Foodly" "Data Source=../database-test.db"
        var connString = configuration.GetConnectionString("Foodly");
        return connString;
    }

    private static AppDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        return dbContext;
    }
}