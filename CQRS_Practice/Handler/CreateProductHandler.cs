using CQRS_Practice.Command;
using CQRS_Practice.Model;
using CQRS_Practice.Repository;
using MediatR;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    private readonly string _imageUploadPath;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        // Set the upload path to the "wwwroot/UploadedImages" directory
        _imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedImages");
        // Ensure the directory exists
        if (!Directory.Exists(_imageUploadPath))
        { 
            Directory.CreateDirectory(_imageUploadPath);
        }
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Generate a unique file name using a GUID
        var fileName = $"{Guid.NewGuid()}_{request.ProductDTO.ImageFile.FileName}";
        var filePath = Path.Combine(_imageUploadPath, fileName);

        // Save the file to the "wwwroot/UploadedImages" directory
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.ProductDTO.ImageFile.CopyToAsync(stream);
        }

        // Save product to the database (including the image path)
        var product = new Product
        {
            Name = request.ProductDTO.Name,
            Price = request.ProductDTO.Price,
            Description = request.ProductDTO.Description,
            ImagePath = Path.Combine("UploadedImages", fileName) // Store relative path
        };

        // Add the product using the repository
        return await _productRepository.AddProductAsync(product);
    }
}
