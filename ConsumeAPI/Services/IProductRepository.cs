using ConsumeAPI.DTO;
using ConsumeAPI.Models;

namespace ConsumeAPI.Services
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<bool> AddProductAsync(ProductDTO productDTO);
        Task<bool> UpdateProductAsync(int id, ProductDTO productDTO);
        Task<bool> DeleteProductAsync(int id);
    }
}
