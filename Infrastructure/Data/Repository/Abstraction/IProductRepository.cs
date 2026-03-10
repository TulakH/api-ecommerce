using Domain;

namespace Infrastructure.Data.Repository.Abstraction;

public interface IProductRepository : IDisposable
{
    IEnumerable<Product> GetProducts();
    IQueryable<Product> GetProductsAsQueryable();
    Task<Product?> GetProductById(Guid id);
    Task<Product?> GetProductByName(string name);
    IQueryable<Product> GetProductsByNameKeyword(string keyword);
    Task InsertProduct(Product product);
    void UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
    Task Save();
}