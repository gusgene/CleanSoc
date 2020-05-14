// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IActivitiesRepository
    {
        #region Methods

        Task<List<Activity>> GetActivities();
        Task<Activity> GetActivity(Guid id);
        Task<bool> Add(Activity activity);
        Task<bool> Update(Activity activity);
        Task<bool> Delete(Activity activity);

        #endregion
    }
}
