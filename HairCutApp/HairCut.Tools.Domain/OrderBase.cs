
namespace HairCut.Tools.Domain
{
    public class OrderBase : Create
    {
        public int UserId { get; set; }
        public double Total { get; set; }
        public Guid OrderRequestId { get; set; }
        public ItemState Status { get; set; }
        public DateTime? ConcludedDate { get; set; }
        public Guid? AccountOrderId { get; set; }

        public enum ItemState
        {
            Pending = 1,
            Waiting = 2,
            Concluded = 3
        }

    }
}
