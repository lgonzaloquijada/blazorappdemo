using blazorappdemo.Models;

namespace blazorappdemo.Services;

public interface IProductService
{
    Task<IEnumerable<Product>?> GetProducts();
    Task<Product?> GetProduct(int id);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
}
