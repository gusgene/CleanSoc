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

    public class Delete
    {
        #region Nested type: Command

        public class Command : IRequest
        {
            #region Properties

            public Guid Id { get; set; }

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
                    throw new Exception("Could not find activity");

                bool success = await _repository.Delete(activity);
                if (success) return Unit.Value;

                throw new Exception("Problem with saving changes");
            }

            #endregion
        }

        #endregion
    }
}
