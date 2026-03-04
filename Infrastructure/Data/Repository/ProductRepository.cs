using Domain;
using Infrastructure.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{

    private bool _disposed = false;

    public async Task DeleteProduct(Guid id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product is null)
            return;
        dbContext.Products.Remove(product);
    }


    public async Task<Product?> GetProductById(Guid id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public async Task<Product?> GetProductByName(string name)
    {
        return await dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public IEnumerable<Product> GetProducts()
    {
        return dbContext.Products.AsEnumerable();
    }

    public async Task InsertProduct(Product product)
    {
        await dbContext.Products.AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        dbContext.Entry(product).State = EntityState.Modified;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

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
}