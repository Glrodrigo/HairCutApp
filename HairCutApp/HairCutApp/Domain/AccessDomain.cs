namespace HairCutApp.Domain
{
    public class AccessDomain
    {
        public int UserId { get; set; }
        public string AccountName { get; set; }
        public string ProfileName { get; set; }
        public int? LevelCode { get; set; }
        public string? Color { get; set; }
    }

    public class AccessChangeParams : AccessDomain
    {
        public Guid Id { get; set; }
    }

    public class AccessChangeUserParams
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid ProfileId { get; set; }
    }
}
