using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IItemRepository
    {
        Task<bool> InsertAsync(ItemBase item);
        Task<List<ItemBase>> FindByIdAsync(int id);
        Task<List<ItemBase>> FindByProductIdAsync(int id);
        Task<List<ItemBase>> FindByUserIdAsync(int userId, ItemBase.ItemState status);
        Task<List<ItemBase>> FindByStatusAndDateAsync(ItemBase.ItemState status, DateTime date, int userId);
        Task<List<ItemBase>> FindByOrderIdAsync(Guid orderRequestid);
        Task<bool> UpdateAsync(ItemBase item);
        Task<bool> DeleteAsync(ItemBase item);
    }
}
