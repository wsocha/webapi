using Microsoft.AspNetCore.Mvc;
using WebAPI.CustomExceptions;
using WebAPI.Extensibility;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;

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
            try
            {
                var result = await productRepository.AddProduct(product);
                logger.LogInformation($"Product #{result.Id} added");
                return Ok(result);
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditProduct(ProductDto product)
        {
            try
            {
                await productRepository.EditProduct(product);
                logger.LogInformation($"Product #{product.Id} edited");
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await productRepository.DeleteProduct(id);
                logger.LogInformation($"Product #{id} deleted");
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}