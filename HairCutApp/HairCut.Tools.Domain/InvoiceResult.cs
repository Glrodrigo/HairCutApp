
namespace HairCut.Tools.Domain
{
    public class InvoiceResult
    {
        public double Total { get; set; }
        public Guid AccountOrderId { get; set; }
        public string IsPayment { get; set; }
        public string ConcludedDate { get; set; }

        public class InvoiceTotalResults
        {
            public List<InvoiceResult> Invoices { get; set; }
            public int TotalPages { get; set; }

            public InvoiceTotalResults()
            {
                Invoices = new List<InvoiceResult>();
            }
        }
    }
}
