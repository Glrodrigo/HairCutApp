
using HairCut.Generals;

namespace HairCut.Tools.Domain
{
    public class AccessBase : Create
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
        public int CreateUserId { get; set; }



        public AccessBase(string accountName, string profileName)
        {

            if (string.IsNullOrEmpty(accountName) || accountName == "string")
                throw new Exception("A conta está em um formato inválido");

            if (string.IsNullOrEmpty(profileName) || profileName == "string")
                throw new Exception("O perfil está em um formato inválido");

            AccountName = HandleFormat.CleanName(accountName.ToUpper());
            ProfileName = HandleFormat.CleanName(profileName.ToUpper());
        }
    }
}
