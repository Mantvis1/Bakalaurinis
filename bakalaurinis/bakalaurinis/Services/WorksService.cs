using AutoMapper;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Services
{
    public class WorksService : IWorksService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly IMapper _mapper;
        private readonly IScheduleGenerationService _scheduleGenerationService;
        private readonly IMessageService _messageService;

        public WorksService(
            IWorksRepository worksRepository,
            IMapper mapper,
            IScheduleGenerationService scheduleGenerationService,
            IMessageService messageService
            )
        {
            _worksRepository = worksRepository;
            _mapper = mapper;
            _scheduleGenerationService = scheduleGenerationService;
            _messageService = messageService;
        }

        public async Task<int> Create(NewWorkDto newActivityDto)
        {
            var work = _mapper.Map<Work>(newActivityDto);
            work.IsAuthor = true;

            var workId = await _worksRepository.Create(work);

            await _scheduleGenerationService.Generate(newActivityDto.UserId);
            await _messageService.Create(work.UserId, workId, MessageTypeEnum.NewActivity);

            return workId;
        }

        public async Task<bool> Delete(int id)
        {
            var work = await _worksRepository.GetById(id);

            if (work == null)
            {
                return false;
            }

            await _messageService.Create(work.UserId, work.Id, MessageTypeEnum.DeleteActivity);

            return await _worksRepository.Delete(work);
        }

        public async Task<ICollection<WorkDto>> GetAll()
        {
            var activities = await _worksRepository.GetAll();
            var activitiesDto = _mapper.Map<WorkDto[]>(activities);

            return activitiesDto;
        }

        public async Task<WorkDto> GetById(int id)
        {
            var work = await _worksRepository.GetById(id);
            var workDto = _mapper.Map<WorkDto>(work);

            return workDto;
        }

        public async Task<ICollection<WorkDto>> GetByUserId(int id)
        {
            var activities = await _worksRepository.FindAllByUserId(id);
            var activitiesDto = _mapper.Map<WorkDto[]>(activities);

            return activitiesDto;
        }

        public async Task<bool> Update(int id, NewWorkDto activityDto)
        {
            if (activityDto == null)
            {
                throw new ArgumentNullException(nameof(activityDto));
            }

            var work = await _worksRepository.GetById(id);

            if (work == null)
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(activityDto, work);

            return await _worksRepository.Update(work);
        }
    }
}
