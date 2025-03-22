using HairCut.Tools.Domain;
using static HairCut.Tools.Domain.OrderResult;

namespace HairCut.Tools.Service
{
    public interface IOrderService
    {
        Task<bool> CreateAsync(int userId);
        Task<List<OrderResult>> FindByIdAsync(int ordertId);
        Task<List<OrderResult>> FindByUserIdAsync(int userId);
        Task<OrderTotalResults> GetByPageAsync(int pageNumber);
        Task<OrderTotalResults> FindByStatusAsync(int pageNumber, OrderBase.ItemState status);
        Task<bool> ChangeStatusAsync(int id, int userId, OrderBase.ItemState status);
        Task<bool> DeleteAsync(int userId, int id);
    }
}
