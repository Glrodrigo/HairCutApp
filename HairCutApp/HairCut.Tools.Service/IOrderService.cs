using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IOrderService
    {
        Task<bool> CreateAsync(int userId);
    }
}
