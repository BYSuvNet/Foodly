using Foodly.Core;
using Microsoft.EntityFrameworkCore;

namespace Foodly.Infrastructure;

public class EFDishRepository : IDishRepository
{
    private readonly AppDbContext _db;

    public EFDishRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Dish>> GetAllAsync()
    {
        return await _db.Dishes.Where(d => d.Name != "Noname").ToListAsync();
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await _db.Dishes.FindAsync(id);
    }

    public async Task<Dish> AddAsync(Dish dish)
    {
        _db.Add(dish);
        await _db.SaveChangesAsync();

        return dish;
    }
}