
namespace HairCut.Tools.Domain
{
    public class UserToken
    {
        public string Token { get; set; }
        public int Id { get; set; }
    }

    public class PasswordByte
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
