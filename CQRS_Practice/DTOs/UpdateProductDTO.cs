namespace CQRS_Practice.DTOs
{
    public class UpdateProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; } // Optional file, can be null if not updating the image
    }
}
