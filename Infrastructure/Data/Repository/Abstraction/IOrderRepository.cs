using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Repository.Abstraction;

public interface IOrderRepository
{

    IQueryable<Order> GetOrdersAsQueryable();
    Task<Order?> GetOrderById(Guid id);
    ValueTask<EntityEntry<Order>> InsertOrder(Order order);
    ValueTask<EntityEntry<OrderItem>> InsertOrderItem(OrderItem orderItem);
    Task DeleteOrderItem(Guid id);
    Task DeleteOrder(Guid id);
    Task Save();
}