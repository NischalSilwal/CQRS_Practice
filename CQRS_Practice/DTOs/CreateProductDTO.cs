﻿public class CreateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile ImageFile { get; set; } // Ensure this is properly defined
}
