using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;
using static HairCut.Tools.Domain.ScheduleResult;

namespace HairCut.Tools.Service
{
    public class ScheduleService : IScheduleService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IScheduleRepository _scheduleRepository;
        private IUserRepository _userRepository { get; set; }

        public ScheduleService(IConfiguration configuration, IScheduleRepository scheduleRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _scheduleRepository = scheduleRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(ScheduleBase schedule)
        {
            try
            {
                var users = await _userRepository.FindByIdAsync(schedule.UserId);

                if (users.Count == 0)
                    throw new Exception("Usuário não localizado em nossa base");

                var user = users[0];

                schedule.ServiceDescription = "Corte de cabelo";
                schedule.CreateDate = DateTime.UtcNow;
                schedule.Active = true;
                schedule.Duration = 45;
                schedule.Email = user.Email;
                schedule.Name = user.Name;
                schedule.Price = double.Parse(_configuration.GetSection("Access")["Price"]);

                return await _scheduleRepository.InsertAsync(schedule);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<ScheduleTotalResults> GetByPageAsync(int pageNumber)
        {
            try
            {
                ScheduleTotalResults result = new ScheduleTotalResults();
                var pageSize = 20;

                if (pageNumber <= 0)
                    throw new Exception("A página está vazia ou inválida");

                var (schedules, totalPages) = await _scheduleRepository.GetByPaginationAsync(pageNumber, pageSize);

                foreach (var sched in schedules)
                {
                    ScheduleResult schedule = new ScheduleResult()
                    {
                        Phone = sched.Phone,
                        ServiceDescription = sched.ServiceDescription,
                        Duration = sched.Duration,
                        Price = sched.Price
                    };

                    if (sched.Done == true)
                        schedule.Done = "Pronto";

                    if (sched.Done != true)
                        schedule.Done = "Aguardando";

                    if (sched.Date != null)
                        schedule.Date = ((DateTime)sched.Date).ToString("dd/MM/yyyy");

                    if (sched.Date == null)
                        schedule.Date = "-";

                    result.Schedules.Add(schedule);
                }

                if (schedules.Count > 0)
                    result.TotalPages = totalPages;

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<ScheduleResult>> FindByUserIdAsync(int userId)
        {
            try
            {
                List<ScheduleResult> result = new List<ScheduleResult>();

                if (userId <= 0)
                    throw new Exception("A key está vazia ou inválida");

                var schedules = await _scheduleRepository.FindByUserIdAsync(userId);

                if (schedules.Count == 0)
                    return result;

                var recent = schedules.FirstOrDefault();

                ScheduleResult schedule = new ScheduleResult()
                {
                    Phone = recent.Phone,
                    ServiceDescription = recent.ServiceDescription,
                    Duration = recent.Duration,
                    Price = recent.Price
                };

                if (recent.Done == true)
                    schedule.Done = "Pronto";

                if (recent.Done != true)
                    schedule.Done = "Aguardando";

                if (recent.Date != null)
                    schedule.Date = ((DateTime)recent.Date).ToString("dd/MM/yyyy");

                if (recent.Date == null)
                    schedule.Date = "-";

                result.Add(schedule);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
