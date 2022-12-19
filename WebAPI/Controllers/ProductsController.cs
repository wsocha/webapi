using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensibility;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IProductRepository productRepository;

        public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
        {
            this.logger = logger;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var result = await productRepository.GetProducts();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(CreateProductDto product)
        {
            var result = await productRepository.AddProduct(product);
            logger.LogInformation($"Product #{result.Id} added");
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> EditProduct(ProductDto product)
        {
            await productRepository.EditProduct(product);
            logger.LogInformation($"Product #{product.Id} edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await productRepository.DeleteProduct(id);
            logger.LogInformation($"Product #{id} deleted");
            return Ok();
        }
    }
}