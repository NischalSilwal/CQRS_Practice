using CQRS_Practice.DTOs;
using CQRS_Practice.Query;
using CQRS_Practice.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Practice.Handler
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

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
