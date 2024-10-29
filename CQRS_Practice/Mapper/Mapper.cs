using CQRS_Practice.DTOs;
using CQRS_Practice.Model;

namespace CQRS_Practice.Mapper
{
    public class Mapper : IMapper
    {
        public ProductDTO MapToDto(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImagePath = product.ImagePath
            };
        }

      
        /*
        public Product MapToEntity(UpdateProductDTO productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                // Additional properties for updating can be added here if necessary
            };
        }
        */
        public Product MapToEntity(ProductDTO productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
            };
        }
    }
}
