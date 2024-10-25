using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Command
{
    public class CreateProductCommand : IRequest<int>
    {
        public CreateProductDTO ProductDTO { get; }

        public CreateProductCommand(CreateProductDTO productDTO)
        {
            ProductDTO = productDTO;
        }
    }
}
