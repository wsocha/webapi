using WebAPI.Models.DTOs;

namespace WebAPI.Extensibility
{
    public interface IProductRepository
    {
        Task<ProductDto> AddProduct(CreateProductDto productDto);

        Task DeleteProduct(int id);

        Task EditProduct(ProductDto productDto);

        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
