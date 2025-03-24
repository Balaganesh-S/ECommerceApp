using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApp.Controllers
{
    
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("api/Products")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productRepository.GetProductsAsync();
            return Ok(products);
        }

        //[HttpGet("{id}")]
        //[Authorize(Roles = "Reader, Writer")]
        //[Route("api/")]
        //public async Task<IActionResult> GetProductById(int id)
        //{
        //    var product = await productRepository.GetProductByIdAsync(id);
        //    if (product == null)
        //        return NotFound();
        //    return Ok(product);
        //}

        [HttpPost]
        [Route("api/categories/{categoryId}/product")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateProduct([FromRoute] int categoryId, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid product data.");
            var createdProductDto = await productRepository.CreateProductAsync(categoryId, productDto);
            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{createdProductDto.Id}", createdProductDto);
        }

        [HttpPut]
        [Route("api/products/{productId}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid product data.");

            var updatedProductDto = await productRepository.UpdateProductAsync(productId,productDto);
            return Ok(updatedProductDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProductDto = await productRepository.DeleteProductAsync(id);
            if (deletedProductDto == null)
                return NotFound();

            return Ok(deletedProductDto);
        }

        [HttpGet]
        [Route("api/products/{categoryId}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var productDtos = await productRepository.GetProductsByCategoryAsync(categoryId);
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("api/products/search/{keyword}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProductsByKeyword(string keyword)
        {
            var productDtos = await productRepository.GetProductByKeywordAsync(keyword);
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("api/products/count")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProductCount()
        {
            var count = await productRepository.GetProductCountAsync();
            return Ok(count);
        }

    }
}
