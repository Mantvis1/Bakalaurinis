using bakalaurinis.Helpers.Interfaces;
using bakalaurinis.Infrastructure.Database.Models;
using System;

namespace bakalaurinis.Helpers
{
    public class ObjectCopier : IObjectCopier
    {
        public ObjectCopier()
        {

        }

        public Work GetCopy(Work work)
        {
            return new Work()
            {
                UserId = work.UserId,
                Title = work.Title,
                WorkPriority = work.WorkPriority,
                Description = work.Description,
                DurationInMinutes = work.DurationInMinutes
            };
        }
    }
}
