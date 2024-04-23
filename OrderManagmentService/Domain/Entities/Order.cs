namespace Domain.Entities;
public class Order
{
    public int Id { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public OrderStatus Status { get; set; }
}