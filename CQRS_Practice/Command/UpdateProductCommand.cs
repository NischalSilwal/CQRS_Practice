using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Command
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; }
        public UpdateProductDTO ProductDTO { get; }

        public UpdateProductCommand(int id, UpdateProductDTO productDTO)
        {
            Id = id;
            ProductDTO = productDTO;
        }
    }
}
