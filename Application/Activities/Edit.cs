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

    public class Edit
    {
        #region Nested type: Command

        public class Command : IRequest
        {
            #region Properties

            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime? Date { get; set; }

            public string City { get; set; }

            public string Venue { get; set; }

            #endregion
        }

        #endregion

        #region Nested type: Handler

        public class Handler : IRequestHandler<Command>
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

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Activity activity = await _repository.GetActivity(request.Id);
                if (activity == null)
                    throw new Exception("Could not find Activity");

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

        #endregion
    }
}
