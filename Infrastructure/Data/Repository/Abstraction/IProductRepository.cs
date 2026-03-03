using Domain;

namespace Infrastructure.Data.Repository.Abstraction;

public interface IProductRepository : IDisposable
{
    IEnumerable<Product> GetProducts();
    Task<Product?> GetProductById(Guid id);
    Task<Product?> GetProductByName(string name);
    Task InsertProduct(Product product);
    void UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
    Task Save();
}