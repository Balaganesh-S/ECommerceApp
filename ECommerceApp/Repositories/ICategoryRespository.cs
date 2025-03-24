using ECommerceApp.Domain;
using ECommerceApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);

        Task<Category> GetCategoryEntityByIdAsync(int categoryId);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto);
        Task<CategoryDto> DeleteCategoryAsync(int id);
    }
}
