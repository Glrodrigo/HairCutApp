using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface ICategoryRepository
    {
        Task<bool> InsertAsync(CategoryBase category);
        Task<List<CategoryBase>> FindByNameAsync(string name);
        Task<List<CategoryBase>> FindByIdAsync(int id);
        Task<List<CategoryBase>> GetAsync();
        Task<List<CategoryBase>> GetByPaginationAsync(int pageNumber, int pageSize);
        Task<bool> UpdateAsync(CategoryBase category);
    }
}
