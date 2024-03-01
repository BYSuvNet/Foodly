namespace Foodly.Core;

public class FoodOrderService : IFoodOrderService
{
    private readonly IDishRepository _dishRepo;
    private readonly IFoodOrderRepository _foodOrderRepo;

    public FoodOrderService(IDishRepository _dishRepo, IFoodOrderRepository _foodOrderRepo)
    {
        this._dishRepo = _dishRepo;
        this._foodOrderRepo = _foodOrderRepo;
    }

    public async Task<string> OrderFood(int dishId)
    {
        Dish? dish = await _dishRepo.GetByIdAsync(dishId);

        if (dish == null)
        {
            return string.Empty;
        }

        FoodOrder newOrder = new() { Dish = dish, OrderDate = DateTime.Now, Price = dish.Price };

        await _foodOrderRepo.AddAsync(newOrder);

        return newOrder.OrderNumber;
    }

    public async Task<List<FoodOrder>> GetAllOrdersAsync()
    {
        return await _foodOrderRepo.GetAllAsync();
    }

    public async Task<FoodOrder?> GetOrderByOrderNoAsync(string orderNo)
    {
        return await _foodOrderRepo.GetOrderByOrderNoAsync(orderNo);
    }
}

