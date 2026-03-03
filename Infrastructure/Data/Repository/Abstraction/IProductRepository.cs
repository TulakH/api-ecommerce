using Domain;

namespace Infrastructure.Data.Repository.Abstraction;

public interface IProductRepository : IDisposable
{
    IEnumerable<Product> GetProducts();
    Product GetProductById(Guid id);
    Product GetProductByName(string name);
    Task InsertProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
    Task Save();
}