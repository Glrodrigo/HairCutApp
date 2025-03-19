using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IUserService
    {
        Task<bool> CreateAsync(string name, string email, string password);
        Task<UserBase> AuthenticateUserAsync(string email, string password);
        Task<UserToken> LoginAsync(string email, string password);
        Task<bool> LogoutAsync(int userId);
        Task<bool> ChangeLoginAsync(int receivedCode, string password, string? email);
        Task<bool> UpdateLoginAsync(UserBase user);
        Task<bool> DeleteAsync(int userId);
    }
}
