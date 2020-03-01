using AutoMapper;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IActivitiesRepository _repository;
        private readonly ITimeService _timeService;
        private readonly IMapper _mapper;

        public ScheduleService(IActivitiesRepository repository, IMapper mapper, ITimeService timeService)
        {
            _repository = repository;
            _mapper = mapper;
            _timeService = timeService;
        }

        public async Task<ICollection<ActivityDto>> GetAllByUserIdFilterByDate(int id)
        {
            var activities = await _repository.FilterByUserIdAndTime(id, _timeService.GetCurrentDay());
            var activitiesDto = _mapper.Map<ActivityDto[]>(activities);

            return activitiesDto;

        }
    }
}
