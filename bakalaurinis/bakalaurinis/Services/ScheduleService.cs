using AutoMapper;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Dtos.Schedule;
using bakalaurinis.Infrastructure.Database.Models;

namespace bakalaurinis.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IWorksRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserSettingsService _userSettingsService;

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

            foreach(var work in worksDto)
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
            int busynessInMinutes = 0;
            
            foreach(var work in works)
            {
                busynessInMinutes += work.DurationInMinutes;
            }

            busynessInMinutes *= 100;
            busynessInMinutes /= 60;
            int workDuration = settings.EndTime - settings.StartTime;

            return busynessInMinutes / workDuration;
        }
    }
}
