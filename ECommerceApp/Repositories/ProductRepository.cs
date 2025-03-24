using AutoMapper;
using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceAppContext dbContext;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public ProductRepository(ECommerceAppContext dbContext,ICategoryRepository categoryRepository ,IMapper mapper) {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(int categoryId, ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }
            Product product = mapper.Map<Product>(productDto);
            Category category = await categoryRepository.GetCategoryEntityByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            product.CategoryId = categoryId;
            product.Category = category;
            category.Products.Add(product);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> DeleteProductAsync(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); 
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return mapper.Map<ProductDto>(product);
        }



        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await dbContext.Products.ToListAsync();
            return mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
            return mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetProductByKeywordAsync(string keyword)
        {
            var products = await dbContext.Products.Where(p => p.Name.Contains(keyword)).ToListAsync();
            return mapper.Map<List<ProductDto>>(products);
        }



        public async Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto)
        {   
            productDto.Id = productId;
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }
            Product product = mapper.Map<Product>(productDto);
            var existingProduct = await dbContext.Products.FindAsync(productId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            // Update properties
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            
           

            await dbContext.SaveChangesAsync(); 

            return mapper.Map<ProductDto>(existingProduct); 
        }

        public async Task<int> GetProductCountAsync()
        {
            return await dbContext.Products.CountAsync();
        }
    }
}
