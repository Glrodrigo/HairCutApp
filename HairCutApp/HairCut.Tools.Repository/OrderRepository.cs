using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(OrderBase order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<OrderBase>> FindByIdAsync(int id)
        {
            try
            {
                var orders = await _context.Orders
                    .Where(t => t.Id == id && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a ordem do banco de dados", ex);
            }
        }

        public async Task<List<OrderBase>> FindByUserIdAsync(int userId)
        {
            try
            {
                var orders = await _context.Orders
                    .Where(t => t.UserId == userId && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a ordem do banco de dados", ex);
            }
        }

        public async Task<List<OrderBase>> FindByIdAsync(int id, OrderBase.ItemState status)
        {
            try
            {
                var orders = await _context.Orders
                    .Where(t => t.Id == id && t.Status != status && t.Active == true)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a ordem do banco de dados", ex);
            }
        }

        public async Task<List<OrderBase>> FindByUserIdAsync(int userId, OrderBase.ItemState status)
        {
            try
            {
                var orders = await _context.Orders
                    .Where(t => t.UserId == userId && t.Status == status && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a ordem do banco de dados", ex);
            }
        }

        public async Task<(List<OrderBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize)
        {
            try
            {
                int totalRecords = await _context.Orders
                    .Where(c => c.Active)
                    .CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Where(p => p.Active == true)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return (orders, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<(List<OrderBase>, int TotalPages)> FindByStatusAsync(int pageNumber, int pageSize, OrderBase.ItemState status)
        {
            try
            {
                int totalRecords = await _context.Orders
                    .Where(p => p.Active && p.Status == status)
                    .CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Where(p => p.Active == true && p.Status == status)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return (orders, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(OrderBase order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar ordem", ex);
            }
        }
    }
}
