using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IProductRepository
    {
        Task<bool> InsertAsync(ProductBase product);
        Task<List<ProductBase>> FindByNameAsync(string name, string brandName, string option);
        Task<List<ProductBase>> GetAsync();
        Task<(List<ProductBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize);
    }
}
