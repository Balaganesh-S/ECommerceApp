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
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return category;
        }


        public async Task<Category> DeleteCategoryAsync(int id)
        {
            Category category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            dbContext.Remove(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> categories = await dbContext.Categories.ToListAsync();
            return categories;
        }


        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            Category category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            return category;
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

        public async Task<Category> UpdateCategoryAsync(int categoryId, Category category)
        {
            Category existingCategory = dbContext.Categories.Find(categoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");
            }
            existingCategory.Name = category.Name;

            await dbContext.SaveChangesAsync();

            return existingCategory;


        }
    }
}
