using HairCut.Tools.Domain;

namespace HairCutApp.Domain
{
    public class InvoiceDomain
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public InvoiceBase.PaymentOptions Payment { get; set; }
    }
}
