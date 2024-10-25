using CQRS_Practice.Command;
using CQRS_Practice.Repository;
using MediatR;

namespace CQRS_Practice.Handler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProductAsync(request.Id);
        }
    }

}
