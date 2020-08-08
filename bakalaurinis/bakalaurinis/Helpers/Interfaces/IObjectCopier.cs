using bakalaurinis.Infrastructure.Database.Models;
using System;

namespace bakalaurinis.Helpers.Interfaces
{
    public interface IObjectCopier
    {
        public Work GetCopy(Work work);
    }
}
