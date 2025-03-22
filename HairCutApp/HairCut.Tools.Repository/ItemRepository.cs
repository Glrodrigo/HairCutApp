using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(ItemBase item)
        {
            try
            {
                await _context.Items.AddAsync(item);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<ItemBase>> FindByIdAsync(int id)
        {
            try
            {
                var items = await _context.Items
                    .Where(t => t.Id == id)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o item do banco de dados", ex);
            }
        }

        public async Task<List<ItemBase>> FindByProductIdAsync(int id)
        {
            try
            {
                var items = await _context.Items
                    .Where(t => t.ItemId == id && t.Status != ItemBase.ItemState.Concluded)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o item do banco de dados", ex);
            }
        }

        public async Task<List<ItemBase>> FindByUserIdAsync(int userId, ItemBase.ItemState status)
        {
            try
            {
                var items = await _context.Items
                    .Where(t => t.UserId == userId && t.Status == status)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o item do banco de dados", ex);
            }
        }

        public async Task<List<ItemBase>> FindByOrderIdAsync(Guid orderRequestid)
        {
            try
            {
                var items = await _context.Items
                    .Where(t => t.OrderRequestId == orderRequestid && t.Status != ItemBase.ItemState.Concluded)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o item do banco de dados", ex);
            }
        }

        public async Task<bool> DeleteAsync(ItemBase item)
        {
            try
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar deletar item", ex);
            }
        }
    }
}
