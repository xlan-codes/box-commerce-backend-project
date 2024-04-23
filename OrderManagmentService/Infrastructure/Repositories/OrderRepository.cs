using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRepository: IOrderRepository
{
    public Task<Order> GetOrderById(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveOrder(Order order)
    {
        throw new NotImplementedException();
    }
}