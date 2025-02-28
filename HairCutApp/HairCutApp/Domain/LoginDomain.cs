
namespace HairCutApp.Domain
{
    public class LoginDomain
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public LoginDomain(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || !HairCut.Generals.StringFormat.isEmail(email))
                throw new Exception("O e-mail está em um formato inválido");

            if (string.IsNullOrEmpty(password))
                throw new Exception("A senha está vazia ou inválida");

            Email = email.ToLower();
            Password = password;
        }
    }
}
