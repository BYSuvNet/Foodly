using Foodly.Core;

namespace UnitTests;

public class FoodOrderConstructor
{
    [Fact]
    public void ShouldSetOrderNumber()
    {

        // Arrange
        // Act
        FoodOrder foodOrder = new FoodOrder();

        //Assert
        Assert.NotNull(foodOrder.OrderNumber);
        Assert.NotEmpty(foodOrder.OrderNumber);
    }

    [Fact]
    public void ShouldSetDeliveryStatusToPending()
    {
        // Arrange
        // Act
        FoodOrder foodOrder = new FoodOrder();

        //Assert
        Assert.Equal(DeliveryStatus.Pending, foodOrder.DeliveryStatus);
    }
}