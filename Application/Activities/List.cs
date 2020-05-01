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
        public class Query : IRequest<List<Activity>>
        {
            
        }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly IActivitiesRepository _repository;

            public Handler(IActivitiesRepository repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = _repository.GetActivities();
                return activities;
            }
        }
    }
}
