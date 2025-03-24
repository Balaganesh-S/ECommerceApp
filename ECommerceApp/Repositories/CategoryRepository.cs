using AutoMapper;
using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceAppContext dbContext;
        private readonly IMapper mapper;
        public CategoryRepository(ECommerceAppContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }
            Category category = mapper.Map<Category>(categoryDto);
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return mapper.Map<CategoryDto>(category);
        }


        public async Task<CategoryDto> DeleteCategoryAsync(int id)
        {
            Category category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            dbContext.Remove(category);
            await dbContext.SaveChangesAsync();
            return mapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            List<Category> categories = await dbContext.Categories.ToListAsync();
            return mapper.Map<List<CategoryDto>>(categories);
        }


        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            Category category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            return mapper.Map<CategoryDto>(category);
        }

        public async Task<Category> GetCategoryEntityByIdAsync(int categoryId)
        {
            Category category = await dbContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");
            }
            return category;
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto)
        {
            categoryDto.Id = categoryId;
            Category existingCategory = dbContext.Categories.Find(categoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");
            }
            existingCategory.Name = categoryDto.Name;

            await dbContext.SaveChangesAsync();

            return mapper.Map<CategoryDto>(existingCategory);


        }
    }
}
