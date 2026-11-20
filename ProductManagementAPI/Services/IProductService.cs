using ProductManagementAPI.Dto;

namespace ProductManagementAPI.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<ProductDto> AddProductAsync(ProductDto product);

        Task<bool> UpdateProductAsync(int id, ProductDto product);

        Task<bool> DeleteProductAsync(int id);
    }
}
