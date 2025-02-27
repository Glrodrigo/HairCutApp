
namespace HairCut.Tools.Service
{
    public interface IUserService
    {
        Task<bool> CreateAsync(string name, string email, string password);
    }
}
