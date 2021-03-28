using AutoMapper;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Services.Generation.Interfaces;
using bakalaurinis.Helpers;
using bakalaurinis.Services.Generation;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Services.Generation.Handlers;

namespace bakalaurinis.Services
{
    public class ScheduleGenerationService : IScheduleGenerationService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IMessageService _messageService;
        private readonly IFactory _factory;
        private readonly IFreeSpaceSaver _freeSpaceSaver;
        private readonly Time _time;

        public ScheduleGenerationService(
            IWorksRepository worksRepository,
            ITimeService timeService,
            IUserSettingsRepository userSettingsRepository,
            IMessageService messageService,
            IFactory factory,
            IFreeSpaceSaver freeSpaceSarver
            )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
            _userSettingsRepository = userSettingsRepository;
            _messageService = messageService;
            _factory = factory;
            _freeSpaceSaver = freeSpaceSarver;
            _time = new Time();

        }

        public async Task<bool> Generate(int userId)
        {
            if ((await _worksRepository.FilterByUserIdAndStartTime(userId)).Any())
            {
                await UpdateSchedule(userId);
                await _messageService.Create(userId, 0, MessageTypeEnum.Generation);
            }

            return false;
        }

        private async Task UpdateSchedule(int userId)
        {
            var userSettings = await _userSettingsRepository.GetByUserId(userId);
            var activitiesToUpdate = (await _worksRepository.FilterByUserIdAndStartTime(userId)).OrderByDescending(x => x.WorkPriority).ToList();
            var handler = new FirstHandler(_worksRepository, _timeService);

            handler.SetNext(new SecondHandler(_worksRepository, _timeService, _freeSpaceSaver, _userSettingsRepository, _factory))
                .SetNext(new ThirdHandler(_worksRepository, _timeService, _userSettingsRepository));

            _time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

            foreach (var activity in activitiesToUpdate)
            {
                await handler.Handle(activity, userId, _time);
            }
        }

        public async Task RecalculateWorkTimeWhenUserChangesSettings(int userId)
        {
            var worksToUpdate = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

            foreach (var workToUpdate in worksToUpdate)
            {
                workToUpdate.StartTime = null;
                workToUpdate.EndTime = null;

                await _worksRepository.Update(workToUpdate);
            }

            await Generate(userId);
        }
    }
}
