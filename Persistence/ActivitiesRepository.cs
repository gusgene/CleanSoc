// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using Microsoft.EntityFrameworkCore;

    public class ActivitiesRepository : IActivitiesRepository
    {
        private DataContext _context;

        public ActivitiesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetActivities()
        {
            var activities = await _context.Activities.ToListAsync();
            return activities;
        }

        public async Task<Activity> GetActivity(Guid id)
        {
            Activity activity = await _context.Activities.FindAsync(id);
            return activity;
        }

        public async Task<int> Add(Activity activity)
        {
            _context.Activities.Add(activity);
            var result =  await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> Update(Activity activity)
        {
            _context.Activities.Update(activity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
