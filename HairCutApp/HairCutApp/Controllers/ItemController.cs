using HairCut.Tools.Domain;
using HairCut.Tools.Service;
using HairCutApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost("create", Name = "createItem")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ItemDomain item)
        {
            try
            {
                ItemBase itemBase = new ItemBase(item.ItemId, item.Quantity);

                var result = await _itemService.CreateAsync(itemBase, item.UserId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByOrderRequestId", Name = "findByOrderRequestId")]
        [Authorize]
        public async Task<IActionResult> FindByOrderIdAsync(Guid id)
        {
            try
            {
                var result = await _itemService.FindByOrderIdAsync(id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpDelete("delete", Name = "deleteItem")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid orderRequestId)
        {
            try
            {
                var result = await _itemService.DeleteAsync(orderRequestId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
