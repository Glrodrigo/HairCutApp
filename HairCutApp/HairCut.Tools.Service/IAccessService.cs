using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IAccessService
    {
        Task<bool> CreateAsync(AccessBase access, int userId);
        Task<bool> ChangeAsync(AccessBase access, int userId, Guid profileId);
        Task<bool> ChangeUserAccessAsync(int id, int userId, Guid profileId);
        Task<bool> DeleteAsync(int userId, Guid profileId);
    }
}
