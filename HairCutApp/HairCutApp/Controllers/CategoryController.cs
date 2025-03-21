using HairCut.Tools.Domain;
using HairCut.Tools.Service;
using HairCutApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create", Name = "createCategory")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDomain category)
        {
            try
            {
                CategoryBase categoryBase = new CategoryBase(category.Name);

                var result = await _categoryService.CreateAsync(categoryBase, category.UserId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("all", Name = "allCategories")]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _categoryService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("byPage", Name = "byPageCategories")]
        [Authorize]
        public async Task<IActionResult> GetByPageAsync(int pageNumber)
        {
            try
            {
                var result = await _categoryService.GetByPageAsync(pageNumber);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpDelete("delete", Name = "deleteCategory")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int userId, int id)
        {
            try
            {
                var result = await _categoryService.DeleteAsync(userId, id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
