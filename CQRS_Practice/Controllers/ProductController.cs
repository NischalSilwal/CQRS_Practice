using Microsoft.AspNetCore.Mvc;
using MediatR;
using CQRS_Practice.Command;
using CQRS_Practice.Query;
using CQRS_Practice.DTOs;
using AutoMapper;

namespace CQRS_Practice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // Create Product (with ImageFile)
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDTO productDTO)
        {
            if (productDTO.ImageFile == null || productDTO.ImageFile.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            var productId = await _mediator.Send(new CreateProductCommand(productDTO));
            return Ok(productId); // Return created product Id
        }

        // Get Product by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
            {
                return NotFound();
            }
          
            return Ok(product);
        }

        // Get All Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
         
            return Ok(products);
        }

        // Update Product (with ImageFile)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductDTO productDTO)
        {
            if (productDTO.ImageFile != null && productDTO.ImageFile.Length == 0)
            {
                return BadRequest("Invalid image file.");
            }

            var result = await _mediator.Send(new UpdateProductCommand(id, productDTO));
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // Delete Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
