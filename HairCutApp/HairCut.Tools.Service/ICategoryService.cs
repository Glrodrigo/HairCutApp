using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryBase category, int userId);
        Task<List<CategoryResult>> GetAsync();
        Task<List<CategoryResult>> GetByPageAsync(int pageNumber);
        Task<bool> DeleteAsync(int userId, int id);
    }
}
