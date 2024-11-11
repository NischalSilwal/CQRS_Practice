using CQRS_Practice.Command;
using CQRS_Practice.Repository;
using CQRS_Practice.Services;
using MediatR;

namespace CQRS_Practice.Handler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService _productService;
        private readonly string _imageUploadPath;

        public DeleteProductHandler(IProductRepository productRepository, IProductService productService)
        {
            _productService = productService;
            _imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedImages");
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the product to access the image path
            var product = await _productService.GetProductByIdAsync(request.Id);
            if (product == null) return false;

            // Get the image file path from the product's ImagePath
            var imagePath = Path.Combine(_imageUploadPath, Path.GetFileName(product.ImagePath));

            // Delete the image file if it exists
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            // Proceed to delete the product from the database
            return await _productService.DeleteProductAsync(request.Id);
        }
    }

}
