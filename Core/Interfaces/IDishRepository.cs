namespace Foodly.Core;

public interface IDishRepository
{
    Task<Dish> AddAsync(Dish dish);
    Task<IEnumerable<Dish>> GetAllAsync();
    Task<Dish?> GetByIdAsync(int id);
}