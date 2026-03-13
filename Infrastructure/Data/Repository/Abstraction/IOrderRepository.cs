using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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