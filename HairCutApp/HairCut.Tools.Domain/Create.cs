
namespace HairCut.Tools.Domain
{
    public abstract class Create : Entity
    {
        public DateTime CreateDate { get; set; }
        public int ChangeUserId { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public bool Active { get; set; }
    }
}
