using Domain;
using Infrastructure.Data;
using Infrastructure.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Repository;

public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task DeleteOrder(Guid id)
    {
        await DeleteOrderItemsFromOrder(id);

        var order = await dbContext.Orders.FindAsync(id);
        if (order is null) return;
        dbContext.Orders.Remove(order);
    }

    public async Task DeleteOrderItem(Guid id)
    {
        var orderItem = await dbContext.OrderItems.FindAsync(id);
        if (orderItem is null) return;
        dbContext.OrderItems.Remove(orderItem);
    }

    public async Task DeleteOrderItemsFromOrder(Guid id)
    {
        if (await dbContext.OrderItems.AnyAsync(i => i.OrderId == id))
        {
            var items = dbContext.OrderItems.Where(i => i.OrderId == id);
            dbContext.OrderItems.RemoveRange(await items.ToListAsync());
        }
    }

    public Task<Order?> GetOrderById(Guid id) => dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

    public IQueryable<Order> GetOrdersAsQueryable() => dbContext.Orders.Include(o => o.OrderItems);

    public ValueTask<EntityEntry<Order>> InsertOrder(Order order) => dbContext.Orders.AddAsync(order);

    public ValueTask<EntityEntry<OrderItem>> InsertOrderItem(OrderItem orderItem) => dbContext.OrderItems.AddAsync(orderItem);

    public Task Save() => dbContext.SaveChangesAsync();
}