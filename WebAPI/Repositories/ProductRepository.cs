using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI.CustomExceptions;
using WebAPI.DAL;
using WebAPI.Extensibility;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;

namespace WebAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiContext _context;

        public ProductRepository(ApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductDto> AddProduct(CreateProductDto productDto)
        {
            if (productDto == null || string.IsNullOrWhiteSpace(productDto.Name) || string.IsNullOrWhiteSpace(productDto.Description))
            {
                throw new ValidationException("Invalid model");
            }

            var newProduct = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
            };
            var result = await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return new ProductDto()
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name,
                Description = result.Entity.Description
            };
        }

        public async Task DeleteProduct(int id)
        {
            var product = new Product()
            {
                Id = id
            };
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                if (e.Message == "Attempted to update or delete an entity that does not exist in the store.") // TODO: Do it without hardocoded string
                {
                    throw new NotFoundException("Product not found");
                }
                throw;
            }
        }

        public async Task EditProduct(ProductDto productDto)
        {
            if (productDto == null || string.IsNullOrWhiteSpace(productDto.Name) || string.IsNullOrWhiteSpace(productDto.Description))
            {
                throw new ValidationException("Invalid model");
            }
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _context.Products
                .Select(p => new ProductDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                })
                .ToListAsync();
        }
    }
}
