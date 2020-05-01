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

    public class Create
    {
        #region Nested type: Command

        public class Command : IRequest
        {
            #region Properties

            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime Date { get; set; }

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
                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    City = request.City,
                    Venue = request.Venue
                };

                bool success = await _repository.Add(activity);
                if (success)
                    return Unit.Value;
                throw new Exception();
            }

            #endregion
        }

        #endregion
    }
}
