
using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IOrderRepository
    {
        Task<bool> InsertAsync(OrderBase order);
        Task<List<OrderBase>> FindByIdAsync(int id);
        Task<List<OrderBase>> FindByUserIdAsync(int userId, OrderBase.ItemState status);
    }
}
