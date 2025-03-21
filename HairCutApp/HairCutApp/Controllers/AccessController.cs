using HairCutApp.Domain;
using HairCut.Tools.Service;
using Microsoft.AspNetCore.Mvc;
using HairCut.Tools.Domain;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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

        [HttpPut("change", Name = "changeAccess")]
        [Authorize]
        public async Task<IActionResult> ChangeAsync([FromBody] AccessChangeParams access)
        {
            try
            {
                AccessBase accessBase = new AccessBase(access.AccountName, access.ProfileName)
                {
                    LevelCode = access.LevelCode,
                    Color = access.Color
                };

                var result = await _accessService.ChangeAsync(accessBase, access.UserId, access.Id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpPut("changeUser", Name = "changeUserAccess")]
        [Authorize]
        public async Task<IActionResult> ChangeUserAccessAsync([FromBody] AccessChangeUserParams access)
        {
            try
            {
                var result = await _accessService.ChangeUserAccessAsync(access.Id, access.UserId, access.ProfileId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpDelete("delete", Name = "deleteProfile")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int userId, Guid profileId)
        {
            try
            {
                var result = await _accessService.DeleteAsync(userId, profileId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
