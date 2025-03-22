using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IOrderRepository
    {
        Task<bool> InsertAsync(OrderBase order);
        Task<List<OrderBase>> FindByIdAsync(int id);
        Task<List<OrderBase>> FindByUserIdAsync(int userId);
        Task<List<OrderBase>> FindByIdAsync(int id, OrderBase.ItemState status);
        Task<List<OrderBase>> FindByUserIdAsync(int userId, OrderBase.ItemState status);
        Task<(List<OrderBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize);
        Task<(List<OrderBase>, int TotalPages)> FindByStatusAsync(int pageNumber, int pageSize, OrderBase.ItemState status);
        Task<bool> UpdateAsync(OrderBase order);
    }
}
