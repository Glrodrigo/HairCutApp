using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IBucketRepository
    {
        Task<bool> InsertAsync(BucketBase bucket);
        Task<List<BucketBase>> FindByIdAsync(int id);
    }
}
