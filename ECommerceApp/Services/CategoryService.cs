using AutoMapper;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using System.Net;

namespace ECommerceApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper ) {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseDto<CategoryResponseDto>> CreateCategoryAsync(CategoryRequestDto categoryRequestDto)
        {
            Category categoryRequest = mapper.Map<Category>(categoryRequestDto);
            Category category = await categoryRepository.CreateCategoryAsync(categoryRequest);
            return new ResponseDto<CategoryResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Created!",
                Data = mapper.Map<CategoryResponseDto>(category)
            };
        }

        public async Task<ResponseDto<CategoryResponseDto>> DeleteCategoryAsync(int id)
        {
            Category category = await categoryRepository.DeleteCategoryAsync(id);
            return new ResponseDto<CategoryResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Deleted!",
                Data = mapper.Map<CategoryResponseDto>(category)
            };
        }

        public async Task<ResponseDto<List<CategoryResponseDto>>> GetCategoriesAsync()
        {
            List<Category> categories = await categoryRepository.GetCategoriesAsync();
            return new ResponseDto<List<CategoryResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Fetched!",
                Data = mapper.Map<List<CategoryResponseDto>>(categories)
            };
        }

        public async Task<ResponseDto<CategoryResponseDto>> GetCategoryByIdAsync(int id)
        {
            Category category = await categoryRepository.GetCategoryByIdAsync(id);
            return new ResponseDto<CategoryResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Fetched!",
                Data = mapper.Map<CategoryResponseDto>(category)
            };

        }

        public async  Task<ResponseDto<CategoryResponseDto>> UpdateCategoryAsync(int categoryId, CategoryRequestDto categoryRequestDto)
        {
            Category categoryRequest = mapper.Map<Category>(categoryRequestDto);
            Category category = await categoryRepository.UpdateCategoryAsync(categoryId, categoryRequest);
            return new ResponseDto<CategoryResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Updated!",
                Data = mapper.Map<CategoryResponseDto>(category)
            };

        }
    }
}
