using ConsumeAPI.DTO;
using ConsumeAPI.Models;
using ConsumeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeAPI.Controllers
{
    public class ConsumeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConsumeController(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            /*
            if (!ModelState.IsValid)
            {
                ViewBag.msg = "Invalid input";
                return View();
            }
            */
            var success = await _productRepository.AddProductAsync(productDTO);
            if (success)
            {
                ViewBag.msg = "Product created successfully!";
                return RedirectToAction("GetAllProduct");
            }

            ViewBag.msg = "Error creating product!";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var productDTO = await _productRepository.GetProductByIdAsync(id);
            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.msg = "Invalid input";
                return View(productDTO);
            }

            var success = await _productRepository.UpdateProductAsync(id, productDTO);
            if (success)
            {
                ViewBag.msg = "Product updated successfully!";
                return RedirectToAction("GetAllProduct");
            }

            ViewBag.msg = "Error updating product!";
            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productRepository.DeleteProductAsync(id);
            if (success)
            {
                ViewBag.msg = "Product deleted successfully!";
            }
            else
            {
                ViewBag.msg = "Error deleting product!";
            }

            return RedirectToAction("GetAllProduct");
        }


    }
}
