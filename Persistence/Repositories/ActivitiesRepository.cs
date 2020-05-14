// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Domain;
    using Domain.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class ActivitiesRepository : IActivitiesRepository
    {
        #region Fields

        private readonly DataContext _context;

        #endregion

        #region Constructors

        public ActivitiesRepository(DataContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<List<Activity>> GetActivities()
        {
            List<Activity> activities = await _context.Activities.ToListAsync();
            return activities;
        }

        public async Task<Activity> GetActivity(Guid id)
        {
            Activity activity = await _context.Activities.FindAsync(id);
            return activity;
        }

        public async Task<bool> Add(Activity activity)
        {
            _context.Activities.Add(activity);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> Update(Activity activity)
        {
            _context.Activities.Update(activity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Activity activity)
        {
            _context.Activities.Remove(activity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion
    }
}
