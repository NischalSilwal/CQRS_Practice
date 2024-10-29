using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Command
{
    public class CreateProductCommand : IRequest<int>
    {
        public ProductDTO ProductDTO { get; }

        public CreateProductCommand(ProductDTO productDTO)
        {
            ProductDTO = productDTO;
        }
    }
}
