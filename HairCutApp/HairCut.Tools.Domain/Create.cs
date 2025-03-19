
namespace HairCut.Tools.Domain
{
    public class Create
    {
        public DateTime CreateDate { get; set; }
        public int ChangeUserId { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public bool Active { get; set; }
    }
}
