using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IUserRepository
    {
        Task<bool> InsertAsync(UserBase user);
    }
}
