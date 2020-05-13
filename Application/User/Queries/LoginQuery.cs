// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.User.Queries
{
    using Domain;

    using MediatR;

    public class LoginQuery : IRequest<User>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
