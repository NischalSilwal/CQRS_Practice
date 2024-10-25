﻿using CQRS_Practice.Command;
using CQRS_Practice.Repository;
using MediatR;
using System.IO;

namespace CQRS_Practice.Handler
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _env;

        public UpdateProductHandler(IProductRepository productRepository, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _env = env;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing product
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if (product == null) return false;

            // Update product details
            product.Name = request.ProductDTO.Name;
            product.Price = request.ProductDTO.Price;
            product.Description = request.ProductDTO.Description;

            // Handle Image Update
            if (request.ProductDTO.ImageFile != null)
            {
                // Define the uploads folder inside wwwroot/UploadedImages
                var uploadsFolder = Path.Combine(_env.WebRootPath, "UploadedImages");
                Directory.CreateDirectory(uploadsFolder);

                // Generate a new unique file name
                var uniqueFileName = $"{Guid.NewGuid()}_{request.ProductDTO.ImageFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, product.ImagePath.TrimStart('/'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                // Save the new image file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProductDTO.ImageFile.CopyToAsync(fileStream);
                }

                // Update the product's ImagePath to the new path
                product.ImagePath = "/UploadedImages/" + uniqueFileName;
            }

            // Update the product in the repository
            return await _productRepository.UpdateProductAsync(product);
        }
    }
}
