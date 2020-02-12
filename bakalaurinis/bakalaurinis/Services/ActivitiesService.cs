using AutoMapper;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IRepository<Activity> _repository;
        private readonly IMapper _mapper;

        public ActivitiesService(IRepository<Activity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(NewActivityDto newActivityDto)
        {
            var activity = _mapper.Map<Activity>(newActivityDto);

            return await _repository.Create(activity);
        }

        public async Task<bool> Delete(int id)
        {
            var activity = await _repository.GetById(id);

            if(activity == null)
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

        public Task<ActivityDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(ActivityDto activityDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
