using Foodly.Core;
using Microsoft.EntityFrameworkCore;

namespace Foodly.Infrastructure;

public class EFFoodOrderRepository : IFoodOrderRepository
{
    private readonly AppDbContext _db;

    public EFFoodOrderRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<FoodOrder> AddAsync(FoodOrder foodOrder)
    {
        _db.Add(foodOrder);
        await _db.SaveChangesAsync();
        return foodOrder;
    }

    public async Task<List<FoodOrder>> GetAllAsync()
    {
        return await _db.FoodOrders.ToListAsync();
    }

    public async Task<FoodOrder?> GetOrderByOrderNoAsync(string orderNo)
    {
        return await _db.FoodOrders.FirstOrDefaultAsync(f => f.OrderNumber == orderNo);
    }
}
