using HairCut.Tools.Domain;
using HairCut.Tools.Service;
using HairCutApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("create", Name = "createSchedule")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ScheduleDomain schedule)
        {
            try
            {
                ScheduleBase scheduleBase = new ScheduleBase(schedule.UserId, schedule.Phone, schedule.Date, schedule.Notes);

                var result = await _scheduleService.CreateAsync(scheduleBase);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("findByUserId", Name = "findScheduleByUserId")]
        [Authorize]
        public async Task<IActionResult> FindByUserIdAsync(int userId)
        {
            try
            {
                var result = await _scheduleService.FindByUserIdAsync(userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("byPage", Name = "byPageSchedules")]
        [Authorize]
        public async Task<IActionResult> GetByPageAsync(int pageNumber)
        {
            try
            {
                var result = await _scheduleService.GetByPageAsync(pageNumber);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
