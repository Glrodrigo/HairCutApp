
namespace HairCut.Tools.Domain
{
    public class BucketBase : Entity
    {
        public Guid ImageId { get; set; }
        public int CreateUserId { get; set; }
        public string Path { get; set; }
        public string? Url { get; set; }
        public bool Success { get; set; }
        public bool Format { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
