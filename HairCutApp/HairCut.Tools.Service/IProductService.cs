using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductBase product, int userId);
        Task<bool> AuthorizationLevel(int userId);
        Task<List<ProductResult>> GetAsync();
        Task<ProductTotalResults> GetByPageAsync(int pageNumber);
        Task<ProductTotalResults> FindByCategoryAsync(int pageNumber, int categoryId);
        Task<List<ProductResult>> FindByIdAsync(int id);
        Task<List<ProductResult>> FindByNameAsync(string name);
        Task<bool> ChangeAsync(ProductParams product);
        Task<bool> DeleteAsync(int userId, int id);
    }
}
