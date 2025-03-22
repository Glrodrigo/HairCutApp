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

        [HttpGet("findById", Name = "findById")]
        [Authorize]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            try
            {
                var result = await _orderService.FindByIdAsync(id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByUserId", Name = "findByUserId")]
        [Authorize]
        public async Task<IActionResult> FindByUserIdAsync(int userId)
        {
            try
            {
                var result = await _orderService.FindByUserIdAsync(userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("byPage", Name = "byPageOrders")]
        [Authorize]
        public async Task<IActionResult> GetByPageAsync(int pageNumber)
        {
            try
            {
                var result = await _orderService.GetByPageAsync(pageNumber);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByStatus", Name = "findByStatusOrders")]
        [Authorize]
        public async Task<IActionResult> FindByStatusAsync(int pageNumber, OrderBase.ItemState status)
        {
            try
            {
                var result = await _orderService.FindByStatusAsync(pageNumber, status);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpPut("change", Name = "changeStatus")]
        public async Task<IActionResult> ChangeAsync(int id, int userId, OrderBase.ItemState status)
        {
            try
            {
                var result = await _orderService.ChangeStatusAsync(id, userId, status);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpDelete("delete", Name = "deleteOrder")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int userId, int id)
        {
            try
            {
                var result = await _orderService.DeleteAsync(userId, id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
