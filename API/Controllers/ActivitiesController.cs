// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Application.Activities;
    using Application.Activities.Commands;
    using Application.Activities.Queries;

    using Domain;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;

   public class ActivitiesController : BaseController
    {
        #region Methods

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List()
        {
            return await Mediator.Send(new ActivitiesListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await Mediator.Send(
                       new DetailsQuery
                       {
                           Id = id
                       });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditCommand command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(
                       new DeleteCommand
                       {
                           Id = id
                       });
        }

        #endregion
    }
}
