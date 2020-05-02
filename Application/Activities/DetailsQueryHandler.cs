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

    using Queries;

    public class DetailsQueryHandler : IRequestHandler<DetailsQuery, Activity>
    {
        #region Fields

        private readonly IActivitiesRepository _repository;

        #endregion

        #region Constructors

        public DetailsQueryHandler(IActivitiesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region Methods

        public Task<Activity> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            Task<Activity> activity = _repository.GetActivity(request.Id);
            return activity;
        }

        #endregion
    }
}
