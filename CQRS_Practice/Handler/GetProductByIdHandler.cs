using CQRS_Practice.DTOs;
using CQRS_Practice.Query;
using CQRS_Practice.Repository;
using CQRS_Practice.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Practice.Handler
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IProductService _productService;

        public GetProductByIdHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                // You might want to throw an exception or return null
                // depending on your use case
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
            }

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImagePath = product.ImagePath
            };
        }
    }
}
