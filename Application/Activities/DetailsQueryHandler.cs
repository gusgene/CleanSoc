// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using Exceptions;

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

        public async Task<Activity> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var activity = await _repository.GetActivity(request.Id);
            
            if (activity == null)
                throw new RestException(HttpStatusCode.NotFound, new{activity = "Not Found"});
            
            return activity;
        }

        #endregion
    }
}
