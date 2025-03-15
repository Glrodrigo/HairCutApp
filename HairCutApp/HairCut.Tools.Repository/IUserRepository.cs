using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IUserRepository
    {
        Task<bool> InsertAsync(UserBase user);
        Task<List<UserBase>> FindByEmailAsync(string email);
        Task<List<UserBase>> FindByIdAsync(int id);
        Task<bool> UpdateAsync(UserBase user);
    }
}
