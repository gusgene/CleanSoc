// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using MediatR;

    public class Details
    {
        public class Query : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly IActivitiesRepository _repository;

            public Handler(IActivitiesRepository repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = _repository.GetActivity(request.Id);
                return activity;
            }
        }
    }
}
