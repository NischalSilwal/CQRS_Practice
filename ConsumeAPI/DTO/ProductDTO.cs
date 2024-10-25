namespace ConsumeAPI.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; } // Image upload
    }

}
