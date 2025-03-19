using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface ICategoryRepository
    {
        Task<bool> InsertAsync(CategoryBase category);
        Task<List<CategoryBase>> FindByNameAsync(string name);
    }
}
