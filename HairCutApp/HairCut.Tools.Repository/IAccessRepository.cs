using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IAccessRepository
    {
        Task<bool> InsertAsync(AccessBase access);
        Task<List<AccessBase>> FindByNameAsync(string accountName, string profileName);
        Task<List<AccessBase>> FindByProfileIdAsync(Guid profileId);
        Task<bool> UpdateAsync(AccessBase access);
    }
}
