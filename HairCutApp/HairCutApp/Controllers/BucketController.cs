using HairCut.Tools.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/bucket")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private readonly IBucketService _bucketService;

        public BucketController(IBucketService bucketService)
        {
            _bucketService = bucketService;
        }

        [HttpPost("create", Name = "createBucket")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(IFormFile file, [FromForm] Guid productImageId, [FromForm] int userId)
        {
            try
            {
                var result = await _bucketService.CreateAsync(file, productImageId, userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
