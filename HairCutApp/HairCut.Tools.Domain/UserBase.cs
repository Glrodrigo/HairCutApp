using HairCut.Generals;

namespace HairCut.Tools.Domain
{
    public class UserBase
    {
        public int Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public UserBase(string name, string email, string password)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 200 || name == "string")
                throw new Exception("O nome está em um formato inválido");

            if (string.IsNullOrEmpty(email) || email == "string")
                throw new Exception("O e-mail está vazio");

            if (!StringFormat.isEmail(email))
                throw new Exception("O e-mail está em um formato inválido");

            if (string.IsNullOrEmpty(password) || password == "string")
                throw new Exception("A senha está vazia ou inválida");

            if (!StringFormat.isValidPassword(password))
                throw new Exception("A Senha precisa conter uma letra maíuscula, um caracter especial e no mínimo cinco caracteres");

            CreationDate = DateTime.UtcNow;
            Name = name.ToUpper();
            Email = email.ToLower();
            Password = password;
        }
    }
}
