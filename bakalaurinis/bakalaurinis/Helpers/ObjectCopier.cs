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
            return work.DeepCopy();
        }
    }
}
