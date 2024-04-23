using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetOrderById(int orderId);
    Task<bool> SaveOrder(Order order);
}