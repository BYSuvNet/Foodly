namespace Foodly.Core;

public interface IFoodOrderRepository
{
    Task<FoodOrder> AddAsync(FoodOrder foodOrder);
    Task<List<FoodOrder>> GetAllAsync();
    Task<FoodOrder?> GetOrderByOrderNoAsync(string orderNo);
}

