using AutoMapper;
using CQRS_Practice.Command;
using CQRS_Practice.DTOs;
using CQRS_Practice.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map from ProductDTO to Product for updating/saving data to the database
        CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore()) // We’ll handle ImagePath separately
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id to prevent modification

        // Map from Product to ProductDTO for retrieving data
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.ImageFile, opt => opt.Ignore()); // ImageFile is used only for input, not in Product

        //// Add the mapping from CreateProductCommand to Product
        //CreateMap<CreateProductCommand, Product>()
        //    .ForMember(dest => dest.ImagePath, opt => opt.Ignore()) // Handle ImagePath separately
        //    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id to prevent modification
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductDTO.Name))
        //    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductDTO.Price))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDTO.Description));
    }
}
