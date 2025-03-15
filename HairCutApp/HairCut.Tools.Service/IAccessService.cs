using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IAccessService
    {
        Task<bool> CreateAsync(AccessBase access, int userId);
    }
}
