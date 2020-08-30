using bakalaurinis.Dtos.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation.Interfaces
{
    public interface INewJobGenerationService
    {
        Task CalculateActivitiesTime(int id, DateTime date, UpdateWorkDto updateActivitiesDto);
    }
}
