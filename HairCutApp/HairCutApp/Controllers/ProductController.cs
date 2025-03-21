using HairCut.Tools.Domain;
using HairCut.Tools.Service;
using HairCutApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create", Name = "createProduct")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDomain product)
        {
            try
            {
                ProductBase productBase = new ProductBase(product.Name, product.BrandName, product.Option, product.Description, product.Price, product.CategoryId, product.Total);

                var result = await _productService.CreateAsync(productBase, product.UserId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("all", Name = "allProducts")]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _productService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("byPage", Name = "byPageProducts")]
        [Authorize]
        public async Task<IActionResult> GetByPageAsync(int pageNumber)
        {
            try
            {
                var result = await _productService.GetByPageAsync(pageNumber);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByCategory", Name = "findByCategoryProducts")]
        [Authorize]
        public async Task<IActionResult> FindByCategoryAsync(int pageNumber, int categoryId)
        {
            try
            {
                var result = await _productService.FindByCategoryAsync(pageNumber, categoryId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findById", Name = "findByIdProduct")]
        [Authorize]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            try
            {
                var result = await _productService.FindByIdAsync(id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByName", Name = "findByNameProduct")]
        [Authorize]
        public async Task<IActionResult> FindByNameAsync(string name)
        {
            try
            {
                var result = await _productService.FindByNameAsync(name);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpPut("changeProduct", Name = "changeProduct")]
        [Authorize]
        public async Task<IActionResult> ChangeProductAsync([FromBody] ProductParams product)
        {
            try
            {
                var result = await _productService.ChangeAsync(product);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpDelete("delete", Name = "deleteProduct")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int userId, int id)
        {
            try
            {
                var result = await _productService.DeleteAsync(userId, id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
