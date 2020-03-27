using AutoMapper;
using bakalaurinis.Constants;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class WorksService : IWorksService
    {
        private readonly IWorksRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;
        private readonly IScheduleGenerationService _scheduleGenerationService;
        private readonly IMessageService _messageService;

        public WorksService(
            IWorksRepository repository,
            IMapper mapper,
            ITimeService timeService,
            IScheduleGenerationService scheduleGenerationService,
            IMessageService messageService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _timeService = timeService;
            _scheduleGenerationService = scheduleGenerationService;
            _messageService = messageService;
        }

        public async Task<int> Create(NewActivityDto newActivityDto)
        {
            var activity = _mapper.Map<Work>(newActivityDto);
            var activityId = await _repository.Create(activity);

            await _messageService.Create(activity.UserId, activityId, MessageTypeEnum.NewActivity);

            return activityId;
        }

        public async Task<bool> Delete(int id)
        {
            var activity = await _repository.GetById(id);

            if (activity == null)
            {
                return false;
            }

            await _messageService.Create(activity.UserId, activity.Id, MessageTypeEnum.DeleteActivity);

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

        public async Task<WorkStatusConfirmationDto> GetWorkConfirmationStatusById(int id)
        {
            var activity = await _repository.GetById(id);
            var activityDto = _mapper.Map<WorkStatusConfirmationDto>(activity);

            return activityDto;
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

        public async Task<bool> Update(int workId, WorkStatusConfirmationDto workConfirmationStatusDto)
        {
            if (workConfirmationStatusDto == null)
            {
                throw new ArgumentNullException(nameof(workConfirmationStatusDto));
            }

            var activity = await _repository.GetById(workId);

            if (activity == null)
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(workConfirmationStatusDto, activity);

            return await _repository.Update(activity);
        }
    }
}
