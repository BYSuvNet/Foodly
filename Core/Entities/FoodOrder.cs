namespace Foodly.Core;

public class FoodOrder
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int DishId { get; set; }
    public Dish? Dish { get; set; }
    public float Price { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Pending;
    public string OrderNumber { get; set; } = string.Empty;

    public FoodOrder()
    {
        OrderNumber = Guid.NewGuid().ToString();
    }
}

public enum DeliveryStatus
{
    Pending,
    Ordered,
    InProgress,
    Ready,
    OutForDelivery,
    Delivered
}