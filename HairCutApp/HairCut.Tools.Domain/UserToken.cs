
namespace HairCut.Tools.Domain
{
    public class UserToken : Entity
    {
        public string Token { get; set; }
    }

    public class PasswordByte
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
