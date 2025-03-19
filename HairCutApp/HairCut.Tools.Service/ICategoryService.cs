using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryBase category, int userId);
    }
}
