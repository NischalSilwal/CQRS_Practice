using Humanizer;

namespace ConsumeAPI.Models
{
    public class Product
    {
        public int Id { get; set; } // Adding an Id to identify products
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; } // For image file uploads

        //  added later To hold the image path for display
        public string ImagePath { get; set; } 
    }
}
