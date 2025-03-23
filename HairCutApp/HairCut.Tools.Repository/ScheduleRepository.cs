using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(ScheduleBase schedule)
        {
            try
            {
                await _context.Schedules.AddAsync(schedule);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<(List<ScheduleBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize)
        {
            try
            {
                int totalRecords = await _context.Schedules
                    .Where(c => c.Active)
                    .CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var schedules = await _context.Schedules
                    .AsNoTracking()
                    .Where(p => p.Active == true)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (schedules, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<List<ScheduleBase>> FindByIdAsync(int id)
        {
            try
            {
                var schedules = await _context.Schedules
                    .Where(t => t.Id == id && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return schedules;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a agenda do banco de dados", ex);
            }
        }

        public async Task<List<ScheduleBase>> FindByUserIdAsync(int userId)
        {
            try
            {
                var schedules = await _context.Schedules
                    .Where(t => t.UserId == userId && t.Active)
                    .OrderByDescending(t => t.CreateDate)
                    .ToListAsync();

                return schedules;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a agenda do banco de dados", ex);
            }
        }
    }
}
