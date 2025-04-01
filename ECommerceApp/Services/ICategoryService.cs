using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryResponseDto>>> GetCategoriesAsync();
        Task<ResponseDto<CategoryResponseDto>> GetCategoryByIdAsync(int id);

        Task<ResponseDto<CategoryResponseDto>> CreateCategoryAsync(CategoryRequestDto categoryRequestDto);
        Task<ResponseDto<CategoryResponseDto>> UpdateCategoryAsync(int categoryId, CategoryRequestDto categoryRequestDto);
        Task<ResponseDto<CategoryResponseDto>> DeleteCategoryAsync(int id);
    }
}
