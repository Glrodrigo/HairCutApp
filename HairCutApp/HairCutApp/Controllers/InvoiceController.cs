using HairCut.Tools.Service;
using HairCutApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("create", Name = "createInvoice")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] InvoiceDomain invoice)
        {
            try
            {
                var result = await _invoiceService.CreateAsync(invoice.UserId, invoice.OrderId, invoice.Payment);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByUserId", Name = "findInvoiceByUserId")]
        [Authorize]
        public async Task<IActionResult> FindByUserIdAsync(int userId)
        {
            try
            {
                var result = await _invoiceService.FindByUserIdAsync(userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("byPage", Name = "byPagePaymentInvoices")]
        [Authorize]
        public async Task<IActionResult> FindByPagePaymentAsync(int pageNumber, int userId)
        {
            try
            {
                var result = await _invoiceService.FindByPagePaymentAsync(pageNumber, userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpPut("payment", Name = "invoicePayment")]
        [Authorize]
        public async Task<IActionResult> ChangeAsync(int userId, Guid accountOrderId)
        {
            try
            {
                var result = await _invoiceService.InvoicePaymentAsync(userId, accountOrderId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
