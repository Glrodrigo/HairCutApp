using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IItemService
    {
        Task<bool> CreateAsync(ItemBase item, int userId);
        Task<List<ItemResult>> FindByOrderIdAsync(Guid orderRequest);
        Task<bool> DeleteAsync(Guid orderRequestId);
    }
}
