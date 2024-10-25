using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Query
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

}
