using AutoMapper;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IWorksRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public ScheduleService(IWorksRepository repository, IMapper mapper, IUserSettingsRepository userSettingsRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userSettingsRepository = userSettingsRepository;
        }

        public async Task<ICollection<WorkDto>> GetAllByUserIdFilterByDate(int id, DateTime date)
        {
            var works = await _repository.FilterByUserIdAndTime(id, date);
            var worksDto = _mapper.Map<WorkDto[]>(works);

            return worksDto;
        }

        public async Task<int> GetBusyness(int id, DateTime date)
        {
            var works = await _repository.FilterByUserIdAndTime(id, date);
            var settings = await _userSettingsRepository.GetByUserId(id);
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
