using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;
using static HairCut.Tools.Domain.InvoiceResult;

namespace HairCut.Tools.Service
{
    public class InvoiceService : IInvoiceService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private IUserRepository _userRepository { get; set; }

        public InvoiceService(IConfiguration configuration, IInvoiceRepository invoiceRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(int userId, int orderId, InvoiceBase.PaymentOptions payment)
        {
            try
            {
                InvoiceBase invoice = new InvoiceBase();

                if (payment != InvoiceBase.PaymentOptions.BankSlip && payment != InvoiceBase.PaymentOptions.CreditCard &&
                    payment != InvoiceBase.PaymentOptions.DebitCard && payment != InvoiceBase.PaymentOptions.Pix)
                {
                    throw new Exception("O pagamento está vazio ou inválido");
                }

                var users = await _userRepository.FindByIdAsync(userId);

                if (users.Count == 0)
                    throw new Exception("Usuário não encontrado");

                var orders = await _orderRepository.FindByIdAsync(orderId);

                if (orders.Count == 0)
                    throw new Exception("A ordem não foi localizada em nossa base");

                var order = orders[0];

                if (order.Status == OrderBase.ItemState.Concluded)
                    throw new Exception("A ordem não pode ser utilizada");

                invoice.AccountOrderRequestId = Guid.NewGuid();
                invoice.OrderId = orderId;
                invoice.UserId = userId;
                invoice.PaymentDescription = Payment(payment);
                invoice.Total = order.Total;
                invoice.Active = true;
                invoice.Status = OrderBase.ItemState.Pending;
                invoice.CreateDate = DateTime.Now;

                order.AccountOrderId = invoice.AccountOrderRequestId;
                order.Status = OrderBase.ItemState.Waiting;
                order.EventDate = invoice.CreateDate;

                await _orderRepository.UpdateAsync(order);

                return await _invoiceRepository.InsertAsync(invoice);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<InvoiceResult>> FindByUserIdAsync(int userId)
        {
            try
            {
                List<InvoiceResult> result = new List<InvoiceResult>();

                if (userId <= 0)
                    throw new Exception("A key está vazia ou inválida");

                var invoices = await _invoiceRepository.FindByUserIdAsync(userId);

                if (invoices.Count == 0)
                    return result;

                var recent = invoices.FirstOrDefault();

                InvoiceResult invoice = new InvoiceResult()
                {
                    AccountOrderId = recent.AccountOrderRequestId,
                    Total = recent.Total
                };

                if (recent.IsPayment == true)
                    invoice.IsPayment = "Pago";

                if (recent.IsPayment != true)
                    invoice.IsPayment = "Pendente";

                if (recent.ConcludedDate != null)
                    invoice.ConcludedDate = ((DateTime)recent.ConcludedDate).ToString("dd/MM/yyyy");

                if (recent.ConcludedDate == null)
                    invoice.ConcludedDate = "-";

                result.Add(invoice);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }


        public async Task<InvoiceTotalResults> FindByPagePaymentAsync(int pageNumber, int userId)
        {
            try
            {
                InvoiceTotalResults result = new InvoiceTotalResults();
                var pageSize = 20;

                if (pageNumber <= 0)
                    throw new Exception("A página está vazia ou inválida");

                var (invoices, totalPages) = await _invoiceRepository.FindByPagePaymentAsync(pageNumber, pageSize, userId);

                foreach (var inv in invoices)
                {
                    InvoiceResult invoice = new InvoiceResult()
                    {
                        AccountOrderId = inv.AccountOrderRequestId,
                        Total = inv.Total
                    };

                    if (inv.IsPayment == true)
                        invoice.IsPayment = "Pago";

                    if (inv.ConcludedDate != null)
                        invoice.ConcludedDate = ((DateTime)inv.ConcludedDate).ToString("dd/MM/yyyy");

                    if (inv.ConcludedDate == null)
                        invoice.ConcludedDate = "-";

                    result.Invoices.Add(invoice);
                }

                if (invoices.Count > 0)
                    result.TotalPages = totalPages;

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> InvoicePaymentAsync(int userId, Guid accountOrderId)
        {
            try
            {
                bool result = false;

                var invoices = await _invoiceRepository.FindByAccountOrderIdAsync(userId, accountOrderId);

                if (invoices.Count == 0)
                    throw new Exception("O invoice não foi localizado em nossa base");

                var invoice = invoices[0];

                invoice.IsPayment = true;
                invoice.PaymentAt = DateTime.UtcNow;
                invoice.EventDate = invoice.PaymentAt;

                result = await _invoiceRepository.UpdateAsync(invoice);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private string Payment(InvoiceBase.PaymentOptions payment)
        {
            if (payment == InvoiceBase.PaymentOptions.BankSlip)
                return "Boleto";

            if (payment == InvoiceBase.PaymentOptions.CreditCard)
                return "Cartão de crédito";

            if (payment == InvoiceBase.PaymentOptions.DebitCard)
                return "Cartão de débito";

            if (payment == InvoiceBase.PaymentOptions.Pix)
                return "Pix";

            return string.Empty;
        }
    }
}
