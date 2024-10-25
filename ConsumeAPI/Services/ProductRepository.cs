using ConsumeAPI.DTO;
using ConsumeAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsumeAPI.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient _client;
        private readonly string BaseURL = "https://localhost:7114/";

        public ProductRepository(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(BaseURL);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var response = await _client.GetAsync("Product");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(result);
            }
            return Enumerable.Empty<Product>();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductDTO>(result);
            }
            return null;
        }

        public async Task<bool> AddProductAsync(ProductDTO productDTO)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(productDTO.Name), "Name" },
                { new StringContent(productDTO.Description), "Description" },
                { new StringContent(productDTO.Price.ToString()), "Price" }
            };

            if (productDTO.ImageFile != null && productDTO.ImageFile.Length > 0)
            {
                var fileContent = new StreamContent(productDTO.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(productDTO.ImageFile.ContentType);
                form.Add(fileContent, "ImageFile", productDTO.ImageFile.FileName);
            }

            var response = await _client.PostAsync("Product", form);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDTO productDTO)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(productDTO.Name), "Name" },
                { new StringContent(productDTO.Description), "Description" },
                { new StringContent(productDTO.Price.ToString()), "Price" }
            };

            if (productDTO.ImageFile != null && productDTO.ImageFile.Length > 0)
            {
                var fileContent = new StreamContent(productDTO.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(productDTO.ImageFile.ContentType);
                form.Add(fileContent, "ImageFile", productDTO.ImageFile.FileName);
            }

            var response = await _client.PutAsync($"Product/{id}", form);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _client.DeleteAsync($"Product/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
