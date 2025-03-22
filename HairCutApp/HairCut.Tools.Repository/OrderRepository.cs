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
                    .Where(t => t.Id == id)
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
                    .Where(t => t.UserId == userId && t.Status == status)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a ordem do banco de dados", ex);
            }
        }
    }
}
