using Domain;
using Infrastructure.Data.Repository.Abstraction;

namespace Infrastructure.Data.Repository;

public class ProductRepository(PostgreDbContext postgreDbContext) : IProductRepository
{


    public async Task DeleteProduct(Guid id)
    {
        var product = await postgreDbContext.Products.FindAsync(id);
        if (product is null)
            return;
        postgreDbContext.Products.Remove(product);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Product GetProductById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Product GetProductByName(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetProducts()
    {
        throw new NotImplementedException();
    }

    public Task InsertProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }

    public Task UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}