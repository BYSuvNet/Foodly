using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foodly.Infrastructure;

public static class FoodlyInfrastructure
{
    public static void AddDbContext(IConfiguration configuration, IServiceCollection services)
    {
        bool useOnlyInMemoryDatabase = false;
        if (configuration["UseInMemoryDatabase"] != null)
        {
            useOnlyInMemoryDatabase = bool.Parse(configuration["UseInMemoryDatabase"]!);
        }

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<AppDbContext>(c => c.UseInMemoryDatabase("Foodly"));
        }
        else
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=../database.db"));
        }
    }
}
