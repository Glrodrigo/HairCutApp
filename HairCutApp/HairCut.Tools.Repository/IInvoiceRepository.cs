using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IInvoiceRepository
    {
        Task<bool> InsertAsync(InvoiceBase invoice);
        Task<List<InvoiceBase>> FindByIdAsync(int id);
        Task<List<InvoiceBase>> FindByUserIdAsync(int userId);
        Task<List<InvoiceBase>> FindByAccountOrderIdAsync(int userId, Guid accountOrderRequestId);
        Task<(List<InvoiceBase>, int TotalPages)> FindByPagePaymentAsync(int pageNumber, int pageSize, int userId);
        Task<bool> UpdateAsync(InvoiceBase invoice);
    }
}
