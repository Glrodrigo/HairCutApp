using Microsoft.AspNetCore.Http;

namespace HairCut.Tools.Service
{
    public interface IBucketService
    {
        Task<bool> CreateAsync(IFormFile file, Guid productImageId, int userId);
    }
}
