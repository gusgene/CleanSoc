// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IActivitiesRepository
    {
        Task<List<Activity>> GetActivities();

        Task<Activity> GetActivity(Guid id);
        Task<int> Add(Activity activity);
    }
}
