namespace HairCutApp.Domain
{
    public class AccessDomain
    {
        public int UserId { get; set; }
        public string AccountName { get; set; }
        public string ProfileName { get; set; }
        public int? LevelCode { get; set; }
        public string? Color { get; set; }

        public AccessDomain(int userId, string accountName, string profileName)
        {
            if (userId == 0)
                throw new Exception("A key está vazia ou inválida");

            if (string.IsNullOrEmpty(accountName) || accountName == "string")
                throw new Exception("A conta está em um formato inválido");

            if (string.IsNullOrEmpty(profileName) || profileName == "string")
                throw new Exception("O perfil está em um formato inválido");

            UserId = userId;
            AccountName = accountName.ToUpper();
            ProfileName = profileName.ToUpper();
        }
    }
}
