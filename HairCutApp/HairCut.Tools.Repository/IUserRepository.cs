using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IUserRepository
    {
        Task<bool> InsertAsync(UserBase user);
        Task<List<UserBase>> FindByEmailAsync(string email);
        Task<bool> UpdateAsync(UserBase user);
    }
}
