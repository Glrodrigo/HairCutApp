using HairCut.Tools.Domain;

namespace HairCut.Tools.Repository
{
    public interface IScheduleRepository
    {
        Task<bool> InsertAsync(ScheduleBase schedule);
        Task<(List<ScheduleBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize);
        Task<List<ScheduleBase>> FindByIdAsync(int id);
        Task<List<ScheduleBase>> FindByUserIdAsync(int userId);
    }
}
