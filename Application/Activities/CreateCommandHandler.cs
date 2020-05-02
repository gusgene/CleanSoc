// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Commands;

    using Domain;

    using MediatR;

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        #region Fields

        private readonly IActivitiesRepository _repository;

        #endregion

        #region Constructors

        public CreateCommandHandler(IActivitiesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region Methods

        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
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

}
