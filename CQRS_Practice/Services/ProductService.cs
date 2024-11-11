using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS_Practice.Model;
using CQRS_Practice.Repository;
using CQRS_Practice.Services;

namespace CQRS_Practice.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            // Additional business logic can go here before saving
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            // Additional logic or error handling can be added here
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            // Additional validation can go here
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            // Check if the product exists before deletion
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            return await _productRepository.DeleteProductAsync(id);
        }
    }
}
