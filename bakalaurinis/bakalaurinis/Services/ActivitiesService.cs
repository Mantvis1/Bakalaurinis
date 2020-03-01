using AutoMapper;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IActivitiesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;
        private readonly IScheduleGenerationService _scheduleGenerationService;

        public ActivitiesService(
            IActivitiesRepository repository,
            IMapper mapper,
            ITimeService timeService,
            IScheduleGenerationService scheduleGenerationService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _timeService = timeService;
            _scheduleGenerationService = scheduleGenerationService;
        }

        public async Task<int> Create(NewActivityDto newActivityDto)
        {
            var activity = _mapper.Map<Activity>(newActivityDto);

            return await _repository.Create(activity);
        }

        public async Task<bool> Delete(int id)
        {
            var activity = await _repository.GetById(id);

            if (activity == null)
            {
                return false;
            }

            return await _repository.Delete(activity);
        }

        public async Task<ICollection<ActivityDto>> GetAll()
        {
            var activities = await _repository.GetAll();
            var activitiesDto = _mapper.Map<ActivityDto[]>(activities);

            return activitiesDto;
        }

        public async Task<ActivityDto> GetById(int id)
        {
            var activity = await _repository.GetById(id);
            var activityDto = _mapper.Map<ActivityDto>(activity);

            return activityDto;
        }

        public async Task<ICollection<ActivityDto>> GetByUserId(int id)
        {
            var activities = await _repository.FindAllByUserId(id);
            var activitiesDto = _mapper.Map<ActivityDto[]>(activities);

            return activitiesDto;
        }

        public async Task<bool> Update(int id, NewActivityDto activityDto)
        {
            if (activityDto == null)
            {
                throw new ArgumentNullException(nameof(activityDto));
            }

            var activity = await _repository.GetById(id);

            if (activity == null)
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(activityDto, activity);

            return await _repository.Update(activity);
        }

        public async Task<bool> Extend(int userId, int activityId)
        {
            var activity = await _repository.GetById(activityId);

            activity.EndTime = _timeService.AddMinutesToTime(activity.EndTime.Value, 10);
            activity.IsExtended = true;

            await _scheduleGenerationService.Update(userId);

            return await _repository.Update(activity);

        }

        public async Task<bool> Finish(int userId, int activityId)
        {
            var activity = await _repository.GetById(activityId);

            activity.IsFinished = true;
            activity.EndTime = _timeService.AddMinutesToTime(DateTime.Now, 0);

            await _scheduleGenerationService.Update(userId);

            return await _repository.Update(activity);
        }
    }
}
