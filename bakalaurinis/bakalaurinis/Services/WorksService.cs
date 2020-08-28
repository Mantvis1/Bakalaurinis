using AutoMapper;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Helpers;

namespace bakalaurinis.Services
{
    public class WorksService : IWorksService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public WorksService(
            IWorksRepository worksRepository,
            IMapper mapper,
            IMessageService messageService
            )
        {
            _worksRepository = worksRepository;
            _mapper = mapper;
            _messageService = messageService;
        }

        public async Task<int> Create(NewWorkDto newworkDto)
        {
            var work = _mapper.Map<Work>(newworkDto);
            work.IsAuthor = true;

            var workId = await _worksRepository.Create(work);

            await _messageService.Create(work.UserId, workId, MessageTypeEnum.NewWork);

            return workId;
        }

        public async Task<bool> Delete(int id)
        {
            var work = await _worksRepository.GetById(id);

            if (CompareValues.IsNull(work))
            {
                return false;
            }

            await _messageService.Create(work.UserId, work.Id, MessageTypeEnum.DeleteWork);

            return await _worksRepository.Delete(work);
        }

        public async Task<ICollection<WorkDto>> GetAll()
        {
            var works = await _worksRepository.GetAll();
            var worksDto = _mapper.Map<WorkDto[]>(works);

            return worksDto;
        }

        public async Task<WorkDto> GetById(int id)
        {
            var work = await _worksRepository.GetById(id);
            var workDto = _mapper.Map<WorkDto>(work);

            return workDto;
        }

        public async Task<ICollection<WorkDto>> GetByUserId(int id)
        {
            var works = await _worksRepository.FindAllByUserId(id);
            var worksDto = _mapper.Map<WorkDto[]>(works);

            return worksDto;
        }

        public async Task<bool> Update(int id, NewWorkDto workDto)
        {
            if (CompareValues.IsNull(workDto))
            {
                throw new ArgumentNullException(nameof(workDto));
            }

            var work = await _worksRepository.GetById(id);

            if (CompareValues.IsNull(work))
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(workDto, work);

            return await _worksRepository.Update(work);
        }
    }
}
