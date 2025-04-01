using ECommerceApp.Domain;
using ECommerceApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);

        Task<Category> GetCategoryEntityByIdAsync(int categoryId);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int categoryId, Category category);
        Task<Category> DeleteCategoryAsync(int id);


    }
}
