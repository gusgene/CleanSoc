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
        #region Nested type: Handler

        public class Handler : IRequestHandler<Query, Activity>
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

            public Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                Task<Activity> activity = _repository.GetActivity(request.Id);
                return activity;
            }

            #endregion
        }

        #endregion

        #region Nested type: Query

        public class Query : IRequest<Activity>
        {
            #region Properties

            public Guid Id { get; set; }

            #endregion
        }

        #endregion
    }
}
