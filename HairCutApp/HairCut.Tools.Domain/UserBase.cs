using HairCut.Generals;

namespace HairCut.Tools.Domain
{
    public class UserBase : Create
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? Hash { get; set; }
        public DateTime? HashDate { get; set; }
        public int AccessCount { get; set; }
        public int AccessFailed { get; set; }
        public bool SignOut { get; set; }
        public bool ResetPassword { get; set; }
        public bool SentResetPasswordCode { get; set; }
        public int ResetPasswordCode { get; set; }
        public Guid SessionId { get; set; }
        public Guid SecurityStamp { get; set; }
        public DateTime? LastAccess { get; set; }
        public Guid? ProfileId { get; set; }
        public int PasswordAttemptCount { get; set; }
        public DateTime? ResetPasswordCreateDate { get; set; }
        public DateTime? AccessDate { get; set; }


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

            CreateDate = DateTime.UtcNow;
            Name = name.ToUpper();
            Email = email.ToLower();
            FirstName = name.Split(' ')[0];
            Active = true;
            Password = password;
        }
    }
}
