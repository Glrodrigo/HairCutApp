
namespace HairCut.Tools.Domain
{
    public class InvoiceBase : Create
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public double Total { get; set; }
        public Guid AccountOrderRequestId { get; set; }
        public OrderBase.ItemState Status { get; set; }
        public PaymentOptions Payment { get; set; }
        public string PaymentDescription { get; set; }
        public bool IsPayment { get; set; }
        public DateTime PaymentAt { get; set; }
        public DateTime? ConcludedDate { get; set; }

        public enum PaymentOptions
        {
            BankSlip = 1,
            CreditCard = 2,
            DebitCard = 3,
            Pix = 3
        }
    }
}
