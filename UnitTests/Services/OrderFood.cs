using Foodly.Core;
using Moq;

namespace UnitTests;

public class OrderFood
{
    Dish dish = new() { Id = 1, Price = 10f };
    Mock<IDishRepository> mockDishRepo = new Mock<IDishRepository>();
    Mock<IFoodOrderRepository> mockFoodOrderRepo = new Mock<IFoodOrderRepository>();
    FoodOrderService foodOrderService;

    public OrderFood()
    {
        mockDishRepo.Setup(repo => repo.GetByIdAsync(dish.Id)).ReturnsAsync(dish);
        foodOrderService = new FoodOrderService(mockDishRepo.Object, mockFoodOrderRepo.Object);
    }

    //State Test
    [Fact]
    public async void ShouldReturnOrderNumberIfDishExists()
    {
        //Act
        string result = await foodOrderService.OrderFood(dish.Id);

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async void ShouldNotReturnOrderNumberIfDishDoesntExists()
    {
        //Arrange
        mockDishRepo.Setup(repo => repo.GetByIdAsync(dish.Id)).ReturnsAsync((Dish?)null);

        //Act
        string result = await foodOrderService.OrderFood(dish.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    //Behaviour Test
    [Fact]
    public async void InvokesDishRepositoryGetByIdAsyncOnce()
    {
        //Act 
        await foodOrderService.OrderFood(dish.Id);

        //Assert
        mockDishRepo.Verify(repo => repo.GetByIdAsync(dish.Id), Times.Once);
    }
}