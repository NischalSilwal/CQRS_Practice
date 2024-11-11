using AutoMapper;
using CQRS_Practice.DTOs;
using CQRS_Practice.Query;
using CQRS_Practice.Repository;
using CQRS_Practice.Services;
using MediatR;

namespace CQRS_Practice.Handler
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public GetAllProductsHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync();
            /*
            return products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImagePath = product.ImagePath
            }).ToList();

            */
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }

}
