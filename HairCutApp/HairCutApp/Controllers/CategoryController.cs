using HairCut.Tools.Domain;
using HairCut.Tools.Service;
using HairCutApp.Domain;
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
    }
}
