//This class is for running migrations with dotnet ef migrations add MyMigration

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Foodly.Infrastructure;

public class MyDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=../database.db");
        return new AppDbContext(optionsBuilder.Options);
    }
}
