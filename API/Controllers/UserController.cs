// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace API.Controllers
{
    using System.Threading.Tasks;

    using Application.User.Queries;

    using Domain;

    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
