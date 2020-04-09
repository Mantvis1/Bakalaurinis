using System;
using System.Collections.Generic;
using System.Text;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Moq;
using Xunit;

namespace test.Tests
{
    public class ScheduleServiceTests
    {
        private readonly ScheduleService _scheduleService;
        private readonly int _count;

        public ScheduleServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            _count = setUp.GetLength("schedule");


            var worksRepository = new WorksRepository(context);
            _scheduleService = new ScheduleService(worksRepository, mapper);
        }

        [Theory]
        [InlineData(1)]
        public async void GetAllByUserId_CountsAreEquals(int id)
        {
            var scheduleLength = (await _scheduleService.GetAllByUserIdFilterByDate(id, DateTime.MinValue)).Count;

            Assert.True(scheduleLength == 1);
        }
    }
}
