using CQRS_Practice.Command;
using CQRS_Practice.Repository;
using CQRS_Practice.Services;
using MediatR;

namespace CQRS_Practice.Handler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService _productService;

        public DeleteProductHandler(IProductRepository productRepository, IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductAsync(request.Id);
        }
    }

}
