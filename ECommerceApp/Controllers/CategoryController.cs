using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService) {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await categoryService.GetCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await categoryService.GetCategoryByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto categoryRequestDto)
        {
            var response = await categoryService.CreateCategoryAsync(categoryRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryRequestDto categoryRequestDto)
        {
            var response = await categoryService.UpdateCategoryAsync(id,categoryRequestDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await categoryService.DeleteCategoryAsync(id);
            return Ok(response);
        }
    }
}
