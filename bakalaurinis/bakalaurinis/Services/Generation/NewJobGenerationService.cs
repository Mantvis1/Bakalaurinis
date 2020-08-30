using AutoMapper;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Generation.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation
{
    public class NewJobGenerationService : INewJobGenerationService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;
        private readonly IMapper _mapper;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public NewJobGenerationService(
            IWorksRepository worksRepository,
            ITimeService timeService,
            IMapper mapper,
            IUserSettingsRepository userSettingsRepository
            )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
            _mapper = mapper;
            _userSettingsRepository = userSettingsRepository;
        }

        public async Task CalculateActivitiesTime(int id, DateTime date, UpdateWorkDto updateActivitiesDto)
        {
            var currentTime = _timeService.AddMinutesToTime(date, (await _userSettingsRepository.GetByUserId(id)).StartTime * (int)TimeEnum.MinutesInHour);

            foreach (var activityDto in updateActivitiesDto.Activities)
            {
                activityDto.StartTime = currentTime;
                currentTime = _timeService.AddMinutesToTime(currentTime, activityDto.DurationInMinutes);
                activityDto.EndTime = currentTime;

                var activity = await _worksRepository.GetById(activityDto.Id);
                _mapper.Map(activityDto, activity);

                await _worksRepository.Update(activity);
            }
        }
    }
}
