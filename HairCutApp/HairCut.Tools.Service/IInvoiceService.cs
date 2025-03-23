using HairCut.Tools.Domain;
using static HairCut.Tools.Domain.InvoiceResult;

namespace HairCut.Tools.Service
{
    public interface IInvoiceService
    {
        Task<bool> CreateAsync(int userId, int orderId, InvoiceBase.PaymentOptions payment);
        Task<List<InvoiceResult>> FindByUserIdAsync(int userId);
        Task<InvoiceTotalResults> FindByPagePaymentAsync(int pageNumber, int userId);
        Task<bool> InvoicePaymentAsync(int userId, Guid accountOrderId);
    }
}
