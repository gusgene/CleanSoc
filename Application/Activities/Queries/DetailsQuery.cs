// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.Activities.Queries
{
    using System;

    using Domain;

    using MediatR;

    public class DetailsQuery : IRequest<Activity>
    {
        #region Properties

        public Guid Id { get; set; }

        #endregion
    }
}
