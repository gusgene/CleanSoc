// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.User.Queries
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using Exceptions;

    using MediatR;

    using Microsoft.AspNetCore.Identity;

    public class LoginQueryHandler : IRequestHandler<LoginQuery, User>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginQueryHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                return new User
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName
                    
                };
            }
            
            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
