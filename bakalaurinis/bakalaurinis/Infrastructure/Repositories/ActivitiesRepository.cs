using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        protected readonly DatabaseContext _context;

        public ActivitiesRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Activity entity)
        {
            _context.Activities.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> Delete(Activity entity)
        {
            _context.Activities.Remove(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<ICollection<Activity>> FilterByUserIdAndStartTime(int id)
        {
            var activities = await _context.Activities.Where(x => x.UserId == id && x.StartTime == null).ToArrayAsync();

            return activities;
        }

        public async Task<ICollection<Activity>> FilterByUserIdAndTime(int id, DateTime today)
        {
            var activities = await _context.Activities.Where(
                x => x.UserId == id &&
                x.StartTime.Value.Year == today.Year &&
                x.StartTime.Value.Month == today.Month &&
                x.StartTime.Value.Day == today.Day
                )
                .OrderBy(x => x.StartTime)
                .ToArrayAsync();

            return activities;
        }

        public async Task<ICollection<Activity>> FindAllByUserId(int id)
        {
            var activities = await _context.Activities.Where(x => x.UserId == id).ToArrayAsync();

            return activities;
        }

        public async Task<ICollection<Activity>> GetAll()
        {
            var activities = await _context.Activities.ToArrayAsync();

            return activities;
        }

        public async Task<Activity> GetById(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            return activity;
        }

        public async Task<bool> Update(Activity entity)
        {
            _context.Activities.Attach(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
