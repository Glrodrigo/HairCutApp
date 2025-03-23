using HairCut.Tools.Domain;
using static HairCut.Tools.Domain.ScheduleResult;

namespace HairCut.Tools.Service
{
    public interface IScheduleService
    {
        Task<bool> CreateAsync(ScheduleBase schedule);
        Task<ScheduleTotalResults> GetByPageAsync(int pageNumber);
        Task<List<ScheduleResult>> FindByUserIdAsync(int userId);
    }
}
