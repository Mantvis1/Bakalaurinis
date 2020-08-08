using bakalaurinis.Helpers;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace test.Tests.HelpersTests
{
    public class ObjectCopyTests
    {
        private readonly ObjectCopier _objectCopier;
        private readonly DatabaseContext _context;

        public ObjectCopyTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

           _context = setUp.DatabaseContext;

            _objectCopier = new ObjectCopier();
        }

        [Fact]
        public async void CheckIfCopyIsNotASameObject()
        {
            var work = (await _context.Works.ToArrayAsync()).FirstOrDefault();

            var workCopy = _objectCopier.GetCopy(work);
            work.Id = 100000;

            Assert.NotEqual(work.Id, workCopy.GetType().GetProperty("Id").GetValue(work));

        }
    }
}
