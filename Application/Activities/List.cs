// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using MediatR;

    public class List
    {
        #region Nested type: Handler

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            #region Fields

            private readonly IActivitiesRepository _repository;

            #endregion

            #region Constructors

            public Handler(IActivitiesRepository repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            #endregion

            #region Methods

            public Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                Task<List<Activity>> activities = _repository.GetActivities();
                return activities;
            }

            #endregion
        }

        #endregion

        #region Nested type: Query

        public class Query : IRequest<List<Activity>>
        {
        }

        #endregion
    }
}
