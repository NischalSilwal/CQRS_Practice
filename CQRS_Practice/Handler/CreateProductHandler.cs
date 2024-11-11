using AutoMapper;
using CQRS_Practice.Command;
//using CQRS_Practice.Mapper;
using CQRS_Practice.Model;
using CQRS_Practice.Repository;
using CQRS_Practice.Services;
using MediatR;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
   
    private readonly IProductService _productService;
    private readonly string _imageUploadPath;
    private readonly IMapper _mapper;

    public CreateProductHandler(IMapper mapper, IProductService productService)
    {
        _productService = productService;
        _mapper = mapper;
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
        var fileName = $"{Guid.NewGuid()}_{request.ProductDTO.ImageFile.FileName}";
        var filePath = Path.Combine(_imageUploadPath, fileName);

        // Save the image to "wwwroot/UploadedImages"
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.ProductDTO.ImageFile.CopyToAsync(stream);
        }

        // Update to use API's correct base URL
        var apiBaseUrl = "https://localhost:7114"; // API URL
        var relativePath = $"/UploadedImages/{fileName}";
        var fullImagePath = $"{apiBaseUrl}{relativePath}";

        
        var product = new Product
        {
            Name = request.ProductDTO.Name,
            Price = request.ProductDTO.Price,
            Description = request.ProductDTO.Description,
            ImagePath = fullImagePath // Full path including API base URL
        };
        
       
      // var product = _mapper.Map<Product>(request);
       return await _productService.AddProductAsync(product);
    }

}
