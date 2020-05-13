// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using MediatR;

    public class ActivitiesListQueryHandler : IRequestHandler<ActivitiesListQuery, List<Activity>>
    {
        #region Fields

        private readonly IActivitiesRepository _repository;

        #endregion

        #region Constructors

        public ActivitiesListQueryHandler(IActivitiesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region Methods

        public Task<List<Activity>> Handle(ActivitiesListQuery request, CancellationToken cancellationToken)
        {
            Task<List<Activity>> activities = _repository.GetActivities();
            return activities;
        }

        #endregion
    }
}
