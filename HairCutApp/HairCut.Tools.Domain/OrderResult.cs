
namespace HairCut.Tools.Domain
{
    public class OrderResult
    {
        public double Total { get; set; }
        public Guid OrderRequestId { get; set; }
        public string Status { get; set; }
        public string ConcludedDate { get; set; }

        public class OrderTotalResults
        {
            public List<OrderResult> Orders { get; set; }
            public int TotalPages { get; set; }

            public OrderTotalResults()
            {
                Orders = new List<OrderResult>();
            }
        }
    }
}
