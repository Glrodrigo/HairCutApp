using Microsoft.EntityFrameworkCore;
using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(UserBase user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<UserBase>> FindByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users
                    .Where(t => t.Email == email)
                    .ToListAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o usuário do banco de dados", ex);
            }
        }

        public async Task<List<UserBase>> FindByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users
                    .Where(t => t.Id == id)
                    .ToListAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o usuário do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(UserBase user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar usuário", ex);
            }
        }
    }
}
