using AutoMapper;
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
        private readonly IScheduleGenerationService _scheduleGenerationService;
        private readonly IMessageService _messageService;

        public WorksService(
            IWorksRepository repository,
            IMapper mapper,
            IScheduleGenerationService scheduleGenerationService,
            IMessageService messageService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _scheduleGenerationService = scheduleGenerationService;
            _messageService = messageService;
        }

        public async Task<int> Create(NewActivityDto newActivityDto)
        {
            var work = _mapper.Map<Work>(newActivityDto);
            work.IsAuthor = true;

            var workId = await _repository.Create(work);

            await _scheduleGenerationService.Generate(newActivityDto.UserId);
            await _messageService.Create(work.UserId, workId, MessageTypeEnum.NewActivity);

            return workId;
        }

        public async Task<bool> Delete(int id)
        {
            var work = await _repository.GetById(id);

            if (work == null)
            {
                return false;
            }

            await _messageService.Create(work.UserId, work.Id, MessageTypeEnum.DeleteActivity);

            return await _repository.Delete(work);
        }

        public async Task<ICollection<ActivityDto>> GetAll()
        {
            var activities = await _repository.GetAll();
            var activitiesDto = _mapper.Map<ActivityDto[]>(activities);

            return activitiesDto;
        }

        public async Task<ActivityDto> GetById(int id)
        {
            var work = await _repository.GetById(id);
            var workDto = _mapper.Map<ActivityDto>(work);

            return workDto;
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

            var work = await _repository.GetById(id);

            if (work == null)
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(activityDto, work);

            return await _repository.Update(work);
        }
    }
}
