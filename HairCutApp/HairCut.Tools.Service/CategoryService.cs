using HairCut.Generals;
using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;

namespace HairCut.Tools.Service
{
    public class CategoryService : ICategoryService
    {
        private IConfiguration _configuration { get; set; }
        private readonly ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository { get; set; }

        public CategoryService(IConfiguration configuration, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(CategoryBase category, int userId)
        {
            try
            {
                var user = await _userRepository.FindByIdAsync(userId);

                if (user.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                category.Name = HandleFormat.CleanName(category.Name);

                var existsCategory = await _categoryRepository.FindByNameAsync(category.Name);

                if (existsCategory.Count > 0)
                    throw new Exception("Categoria de produto existente");

                category.CreateDate = DateTime.UtcNow;
                category.Active = true;
                category.CreateUserId = userId;

                return await _categoryRepository.InsertAsync(category);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            try
            {
                if (userId == 0 || id == 0)
                    throw new Exception("A key está vazia ou inválida");

                var user = await _userRepository.FindByIdAsync(userId);

                if (user.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                var categories = await _categoryRepository.FindByIdAsync(id);

                if (categories.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                var category = categories[0];

                if (category.Active == false)
                    throw new Exception("Categoria desativada");

                category.Active = false;
                category.ExclusionDate = DateTime.UtcNow;
                category.EventDate = category.ExclusionDate;
                category.ChangeUserId = userId;

                var result = await _categoryRepository.UpdateAsync(category);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
