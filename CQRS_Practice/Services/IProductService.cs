using CQRS_Practice.DTOs;
using CQRS_Practice.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS_Practice.Services
{
    public interface IProductService
    {
        Task<int> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
