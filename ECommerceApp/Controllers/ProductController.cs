using Azure;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using ECommerceApp.Services;
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
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("api/Products")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await productService.GetProductsAsync();
            return StatusCode((int)response.StatusCode, response);
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
        public async Task<IActionResult> CreateProduct([FromRoute] int categoryId, [FromBody] ProductRequestDto productRequestDto)
        {
            var response = await productService.CreateProductAsync(categoryId, productRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        [Route("api/products/{productId}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] ProductRequestDto productRequestDto)
        {
            var response = await productService.UpdateProductAsync(productId,productRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.DeleteProductAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [Route("api/products/{categoryId}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var response = await productService.GetProductsByCategoryAsync(categoryId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [Route("api/products/search/{keyword}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProductsByKeyword(string keyword)
        {
            var response = await productService.GetProductByKeywordAsync(keyword);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [Route("api/products/count")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProductCount()
        {
            var response = await productService.GetProductCountAsync();
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
