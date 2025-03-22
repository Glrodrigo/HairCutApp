using HairCut.Tools.Domain;
using HairCut.Tools.Service;
using HairCutApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create", Name = "createOrder")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] OrderDomain order)
        {
            try
            {
                var result = await _orderService.CreateAsync(order.UserId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
