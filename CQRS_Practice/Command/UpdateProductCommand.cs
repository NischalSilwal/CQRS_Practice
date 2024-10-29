using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Command
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; }
        public ProductDTO ProductDTO { get; }

        public UpdateProductCommand(int id, ProductDTO productDTO)
        {
            Id = id;
            ProductDTO = productDTO;
        }
    }
}
