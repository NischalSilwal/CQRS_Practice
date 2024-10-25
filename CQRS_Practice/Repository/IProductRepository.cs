using CQRS_Practice.Model;

namespace CQRS_Practice.Repository
{
    public interface IProductRepository
    {
        Task<int> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }

}
