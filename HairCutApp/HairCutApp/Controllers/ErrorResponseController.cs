using HairCutApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    public static class ErrorResponseController
    {
        public static async Task<IActionResult> CreateExceptionResponse(this ControllerBase controller, Exception error)
        {
            var errors = new List<ErrorDomain>();
            errors.Add(ErrorDomain.GetError(error.Message));

            return await Task.FromResult(controller.BadRequest(errors));
        }
    }
}
