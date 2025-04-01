using AutoMapper;
using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

        public async Task<Product> CreateProductAsync(int categoryId, Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
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
            return product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); 
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return product;
        }



        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await dbContext.Products.ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductByKeywordAsync(string keyword)
        {
            var products = await dbContext.Products.Where(p => p.Name.Contains(keyword)).ToListAsync();
            return products;
        }



        public async Task<Product> UpdateProductAsync(int productId, Product product)
        {   
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
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

            return existingProduct;
        }

        public async Task<int> GetProductCountAsync()
        {
            return await dbContext.Products.CountAsync();
        }

        public async Task<int> GetProductQuantityByID(int productId)
        {
            Product product = await dbContext.Products.FindAsync(productId);
            return product.Quantity;
        }
        public async Task DecreaseProductQuantityAsync(int productId, int quantity)
        {
            Product product = await dbContext.Products.FindAsync(productId);
            product.Quantity=product.Quantity-quantity;
            await dbContext.SaveChangesAsync();
        }
    }
}
