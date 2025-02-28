using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IAuthenticateService
    {
        Task<UserBase> AuthenticateAsync(string email, string password, List<UserBase> user);
        string GenerateToken(int id, string email);
        PasswordByte PasswordByteAsync(UserBase user);
    }
}
