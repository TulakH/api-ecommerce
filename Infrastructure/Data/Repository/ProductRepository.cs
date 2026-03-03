using Domain;
using Infrastructure.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ProductRepository(PostgreDbContext postgreDbContext) : IProductRepository
{

    private bool _disposed = false;

    public async Task DeleteProduct(Guid id)
    {
        var product = await postgreDbContext.Products.FindAsync(id);
        if (product is null)
            return;
        postgreDbContext.Products.Remove(product);
    }


    public async Task<Product?> GetProductById(Guid id)
    {
        return await postgreDbContext.Products.FindAsync(id);
    }

    public async Task<Product?> GetProductByName(string name)
    {
        return await postgreDbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public IEnumerable<Product> GetProducts()
    {
        return postgreDbContext.Products.AsEnumerable();
    }

    public async Task InsertProduct(Product product)
    {
        await postgreDbContext.Products.AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        postgreDbContext.Entry(product).State = EntityState.Modified;
    }

    public async Task Save()
    {
        await postgreDbContext.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                postgreDbContext.Dispose();
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