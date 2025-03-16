using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class AccessRepository : IAccessRepository
    {
        private readonly AppDbContext _context;

        public AccessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(AccessBase access)
        {
            try
            {
                await _context.Access.AddAsync(access);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<AccessBase>> FindByNameAsync(string accountName, string profileName)
        {
            try
            {
                var accesses = await _context.Access
                    .Where(t => t.AccountName == accountName || t.ProfileName == profileName)
                    .ToListAsync();

                return accesses;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o acesso do banco de dados", ex);
            }
        }

        public async Task<List<AccessBase>> FindByProfileIdAsync(Guid profileId)
        {
            try
            {
                var accesses = await _context.Access
                    .Where(t => t.ProfileId == profileId)
                    .ToListAsync();

                return accesses;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o acesso do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(AccessBase access)
        {
            try
            {
                _context.Access.Update(access);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar acesso", ex);
            }
        }
    }
}
