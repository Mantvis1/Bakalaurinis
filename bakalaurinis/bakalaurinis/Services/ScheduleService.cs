using AutoMapper;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Dtos.Schedule;
using bakalaurinis.Infrastructure.Enums;
using System.Linq;

namespace bakalaurinis.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IWorksRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserSettingsService _userSettingsService;
        private readonly int maxPersentageValue = 100;

        public ScheduleService(IWorksRepository repository, IMapper mapper, IUserSettingsService userSettingsService)
        {
            _repository = repository;
            _mapper = mapper;
            _userSettingsService = userSettingsService;
        }

        public async Task<GetScheduleDto> GetAllByUserIdFilterByDate(int userId, DateTime date)
        {
            var works = await _repository.FilterByUserIdAndTime(userId, date);
            var worksDto = _mapper.Map<WorkDto[]>(works);
            var scheduleDto = new GetScheduleDto();

            foreach (var work in worksDto)
            {
                scheduleDto.works.Add(work);
            }

            scheduleDto.Busyness = await GetBusyness(userId, date);
            scheduleDto.StartTime = (await _userSettingsService.GetByUserId(userId)).StartTime;
            scheduleDto.EndTime = (await _userSettingsService.GetByUserId(userId)).EndTime;

            return scheduleDto;
        }

        public async Task<int> GetBusyness(int userId, DateTime date)
        {
            var works = await _repository.FilterByUserIdAndTime(userId, date);
            var settings = await _userSettingsService.GetByUserId(userId);
            var busynessInMinutes = works.Sum(work => work.DurationInMinutes);

            var percentageBusyness = busynessInMinutes * maxPersentageValue / (int)TimeEnum.MinutesInHour / (settings.EndTime - settings.StartTime);

            return percentageBusyness;
        }
    }
}
