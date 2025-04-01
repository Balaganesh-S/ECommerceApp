using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(int categoryId, Product product);
        Task<Product> UpdateProductAsync(int productId, Product product);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> DeleteProductAsync(int id);
        Task<List<Product>> GetProductByKeywordAsync(string keyword);
        Task<int> GetProductCountAsync();

        Task<int> GetProductQuantityByID(int productId);

        Task DecreaseProductQuantityAsync(int productId, int quantity);
    }
}
