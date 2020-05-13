// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities.Commands
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using Exceptions;

    using MediatR;

    public class EditCommandHandler : IRequestHandler<EditCommand>
    {
        #region Fields

        private readonly IActivitiesRepository _repository;

        #endregion

        #region Constructors

        public EditCommandHandler(IActivitiesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region Methods

        public async Task<Unit> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            Activity activity = await _repository.GetActivity(request.Id);
            
            if (activity == null)
                throw new RestException(HttpStatusCode.NotFound, new{activity = "Not Found"});

            activity.Title = request.Title ?? activity.Title;
            activity.Description = request.Description ?? activity.Description;
            activity.Category = request.Category ?? activity.Category;
            activity.Date = request.Date ?? activity.Date;
            activity.City = request.City ?? activity.City;
            activity.Venue = request.Venue ?? activity.Venue;

            bool success = await _repository.Update(activity);
            if (success)
                return Unit.Value;

            throw new Exception("Problem With Saving");
        }

        #endregion
    }
}
