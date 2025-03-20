using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class BucketRepository : IBucketRepository
    {
        private readonly AppDbContext _context;

        public BucketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(BucketBase bucket)
        {
            try
            {
                await _context.Buckets.AddAsync(bucket);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<BucketBase>> FindByIdAsync(int id)
        {
            try
            {
                var bucket = await _context.Buckets
                    .Where(t => t.Id == id)
                    .ToListAsync();

                return bucket;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o bucket do banco de dados", ex);
            }
        }
    }
}
