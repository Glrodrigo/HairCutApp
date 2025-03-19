using HairCut.Tools.Domain;

namespace HairCut.Tools.Service
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductBase product, int userId);
        Task<bool> AuthorizationLevel(int userId);
        Task<List<ProductResult>> GetAsync();
        Task<ProductTotalResults> GetByPageAsync(int pageNumber);
    }
}
