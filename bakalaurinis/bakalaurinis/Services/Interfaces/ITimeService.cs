using System;

namespace bakalaurinis.Infrastructure.Utils.Interfaces
{
    public interface ITimeService
    {
        DateTime GetCurrentDay();
        DateTime GetDateTime(int? minutes);
    }
}
