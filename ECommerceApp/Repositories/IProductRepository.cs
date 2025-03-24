using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(int categoryId, ProductDto productDto);
        Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto);
        Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDto> DeleteProductAsync(int id);
        Task<List<ProductDto>> GetProductByKeywordAsync(string keyword);
        Task<int> GetProductCountAsync();
    }
}
