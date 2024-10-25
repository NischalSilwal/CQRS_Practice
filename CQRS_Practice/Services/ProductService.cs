using CQRS_Practice.DTOs;
using CQRS_Practice.Model;
using CQRS_Practice.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_Practice.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Add a new product
        public async Task<int> AddProductAsync(CreateProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Description = productDTO.Description,
                // Add more properties if needed
            };

            return await _productRepository.AddProductAsync(product);
        }

        // Get a product by its ID
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImagePath = product.ImagePath
            };
        }

        // Get all products
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImagePath = product.ImagePath
            });
        }

        // Update a product
        public async Task<bool> UpdateProductAsync(int id, ProductDTO updateProductDTO)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null) return false;

            existingProduct.Name = updateProductDTO.Name;
            existingProduct.Price = updateProductDTO.Price;
            existingProduct.Description = updateProductDTO.Description;
            // Update more properties if needed

            return await _productRepository.UpdateProductAsync(existingProduct);
        }

        // Delete a product
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

     
    }
}
