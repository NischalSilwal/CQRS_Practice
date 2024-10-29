using CQRS_Practice.DTOs;
using CQRS_Practice.Model;

namespace CQRS_Practice.Mapper
{
    public interface IMapper
    {
        ProductDTO MapToDto(Product product);
        Product MapToEntity(ProductDTO productDto);
    }

}
