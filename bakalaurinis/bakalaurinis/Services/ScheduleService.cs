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

        public ScheduleService(IWorksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<WorkDto>> GetAllByUserIdFilterByDate(int id, DateTime date)
        {
            var activities = await _repository.FilterByUserIdAndTime(id, date);
            var activitiesDto = _mapper.Map<WorkDto[]>(activities);

            return activitiesDto;

        }
    }
}
