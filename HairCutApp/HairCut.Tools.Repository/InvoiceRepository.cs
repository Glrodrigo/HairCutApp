using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(InvoiceBase invoice)
        {
            try
            {
                await _context.Invoices.AddAsync(invoice);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<InvoiceBase>> FindByIdAsync(int id)
        {
            try
            {
                var invoices = await _context.Invoices
                    .Where(t => t.Id == id && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a invoice do banco de dados", ex);
            }
        }

        public async Task<List<InvoiceBase>> FindByUserIdAsync(int userId)
        {
            try
            {
                var orders = await _context.Invoices
                    .Where(t => t.UserId == userId && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a invoice do banco de dados", ex);
            }
        }

        public async Task<List<InvoiceBase>> FindByAccountOrderIdAsync(int userId, Guid accountOrderRequestId)
        {
            try
            {
                var orders = await _context.Invoices
                    .Where(t => t.UserId == userId && t.AccountOrderRequestId == accountOrderRequestId && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a invoice do banco de dados", ex);
            }
        }

        public async Task<(List<InvoiceBase>, int TotalPages)> FindByPagePaymentAsync(int pageNumber, int pageSize, int userId)
        {
            try
            {
                int totalRecords = await _context.Invoices
                    .Where(c => c.IsPayment == true && c.UserId == userId && c.Active)
                    .CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var invoices = await _context.Invoices
                    .AsNoTracking()
                    .Where(c => c.IsPayment == true && c.UserId == userId && c.Active)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return (invoices, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(InvoiceBase invoice)
        {
            try
            {
                _context.Invoices.Update(invoice);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar invoice", ex);
            }
        }
    }
}
