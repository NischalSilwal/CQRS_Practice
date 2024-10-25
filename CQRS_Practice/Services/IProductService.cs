using CQRS_Practice.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS_Practice.Services
{
    public interface IProductService
    {
        Task<int> AddProductAsync(CreateProductDTO productDTO);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(int id, ProductDTO updateProductDTO);
        Task<bool> DeleteProductAsync(int id);
    }
}
