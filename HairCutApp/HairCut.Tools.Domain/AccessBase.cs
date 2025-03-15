
namespace HairCut.Tools.Domain
{
    public class AccessBase
    {
        public int Id { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public Guid ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int? LevelCode { get; set; }
        public int RuleCode { get; set; }
        public string RuleName { get; set; }
        public string Color { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public int ChangeUserId { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public bool Active { get; set; }


        public AccessBase(string accountName, string profileName)
        {

            if (string.IsNullOrEmpty(accountName) || accountName == "string")
                throw new Exception("A conta está em um formato inválido");

            if (string.IsNullOrEmpty(profileName) || profileName == "string")
                throw new Exception("O perfil está em um formato inválido");

            AccountName = accountName.ToUpper();
            ProfileName = profileName.ToUpper();
        }
    }
}
