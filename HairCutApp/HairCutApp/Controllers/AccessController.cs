using HairCutApp.Domain;
using HairCut.Tools.Service;
using Microsoft.AspNetCore.Mvc;
using HairCut.Tools.Domain;

namespace HairCutApp.Controllers
{
    [Route("v1/access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        [HttpPost("create", Name = "createAccess")]
        public async Task<IActionResult> CreateAsync([FromBody] AccessDomain access)
        {
            try
            {
                AccessBase accessBase = new AccessBase(access.AccountName, access.ProfileName)
                {
                    LevelCode = access.LevelCode,
                    Color = access.Color
                };

                var result = await _accessService.CreateAsync(accessBase, access.UserId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
