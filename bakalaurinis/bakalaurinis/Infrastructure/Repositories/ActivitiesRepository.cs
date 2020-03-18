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
        public async Task<int> Create(Work entity)
        {
            _context.Works.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> Delete(Work entity)
        {
            _context.Works.Remove(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<ICollection<Work>> FilterByUserIdAndStartTime(int id)
        {
            var activities = await _context.Works.Where(x => x.UserId == id && x.StartTime == null).ToArrayAsync();

            return activities;
        }

        public async Task<ICollection<Work>> FilterByUserIdAndStartTimeIsNotNull(int id)
        {
            var activities = await _context.Works.Where(x => x.UserId == id && x.StartTime != null).ToArrayAsync();

            return activities;
        }

        public async Task<ICollection<Work>> FilterByUserIdAndTime(int id, DateTime today)
        {
            var activities = await _context.Works.Where(
                x => x.UserId == id &&
                x.StartTime.Value.Year == today.Year &&
                x.StartTime.Value.Month == today.Month &&
                x.StartTime.Value.Day == today.Day
                )
                .OrderBy(x => x.StartTime)
                .ToArrayAsync();

            return activities;
        }

        public async Task<ICollection<Work>> FindAllByUserId(int id)
        {
            var activities = await _context.Works.Where(x => x.UserId == id).ToArrayAsync();

            return activities;
        }

        public async Task<Work> FindLastByUserIdAndStartTime(int userId)
        {
            var activity = (await _context.Works.Where(x => x.UserId == userId && x.StartTime != null).ToArrayAsync())
                .OrderBy(x => x.StartTime)
                .LastOrDefault();

            return activity;
        }

        public async Task<ICollection<Work>> GetAll()
        {
            var activities = await _context.Works.ToArrayAsync();

            return activities;
        }

        public async Task<Work> GetById(int id)
        {
            var activity = await _context.Works.FindAsync(id);

            return activity;
        }

        public async Task<bool> Update(Work entity)
        {
            _context.Works.Attach(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
