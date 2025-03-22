using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IItemRepository
    {
        Task<bool> InsertAsync(ItemBase item);
        Task<List<ItemBase>> FindByIdAsync(int id);
        Task<List<ItemBase>> FindByProductIdAsync(int id);
        Task<List<ItemBase>> FindByUserIdAsync(int userId, ItemBase.ItemState status);
        Task<List<ItemBase>> FindByOrderIdAsync(Guid orderRequestid);
        Task<bool> DeleteAsync(ItemBase item);
    }
}
