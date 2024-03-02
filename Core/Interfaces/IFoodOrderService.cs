namespace Foodly.Core;

public interface IFoodOrderService
{
    Task<string> OrderFoodAsync(int dishId);
    Task<List<FoodOrder>> GetAllOrdersAsync();
    Task<FoodOrder?> GetOrderByOrderNoAsync(string orderNo);
}
