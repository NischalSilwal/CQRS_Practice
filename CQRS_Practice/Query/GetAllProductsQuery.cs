using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Query
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>> { }

}
