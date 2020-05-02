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

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        #region Fields

        private readonly IActivitiesRepository _repository;

        #endregion

        #region Constructors

        public DeleteCommandHandler(IActivitiesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region Methods

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
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
}
