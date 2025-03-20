using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IProductRepository
    {
        Task<bool> InsertAsync(ProductBase product);
        Task<List<ProductBase>> FindAsync(string name, string brandName, string option);
        Task<List<ProductBase>> FindByNameAsync(string name);
        Task<List<ProductBase>> FindByIdAsync(int id);
        Task<List<ProductBase>> GetAsync();
        Task<(List<ProductBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize);
        Task<(List<ProductBase>, int TotalPages)> FindByCategoryAsync(int pageNumber, int pageSize, int categoryId);
        Task<bool> UpdateAsync(ProductBase product);
    }
}
