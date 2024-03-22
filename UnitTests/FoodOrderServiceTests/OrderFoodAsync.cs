using Moq;
using System.Threading.Tasks;
using Xunit;
using Foodly.Core;

namespace UnitTests;

public class OrderFoodAsync
{
    [Fact]
    public async void ReturnsOrderNumberWhenDishExists()
    {
        // Arrange
        var dishId = 1;
        var dish = new Dish { Id = dishId, Price = 10.00f }; // Antag att detta är din Dish-entitet.
        var mockDishRepo = new Mock<IDishRepository>();
        var mockFoodOrderRepo = new Mock<IFoodOrderRepository>();

        mockDishRepo.Setup(repo => repo.GetByIdAsync(dishId)).ReturnsAsync(dish);
        mockFoodOrderRepo.Setup(repo => repo.AddAsync(It.IsAny<FoodOrder>()))
                         .Returns(Task.FromResult<FoodOrder>(null));

        var service = new FoodOrderService(mockDishRepo.Object, mockFoodOrderRepo.Object);

        // Act
        var result = await service.OrderFoodAsync(dishId);

        // Assert
        Assert.False(string.IsNullOrEmpty(result)); // Kontrollera att ett ordernummer returneras
    }

    [Fact]
    public async Task OrderFoodAsync_CallsGetByIdAsyncOnDishRepo()
    {
        // Arrange
        var dishId = 1; // Exempel-ID för att använda i testet
        var dish = new Dish { Id = dishId, Price = 10.00f }; // Antag att detta är din Dish-entitet

        var mockDishRepo = new Mock<IDishRepository>();
        var mockFoodOrderRepo = new Mock<IFoodOrderRepository>();

        mockDishRepo.Setup(repo => repo.GetByIdAsync(dishId)).ReturnsAsync(dish);
        var service = new FoodOrderService(mockDishRepo.Object, mockFoodOrderRepo.Object);

        // Act
        await service.OrderFoodAsync(dishId);

        // Assert
        mockDishRepo.Verify(repo => repo.GetByIdAsync(dishId), Times.Once);
    }
}