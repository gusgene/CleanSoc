// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.User.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;
    using Domain.Repositories;

    using MediatR;

    public class CurrentUserQueryHandler : IRequestHandler<CurrentUserQuery, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserAccessor _userAccessor;

        public CurrentUserQueryHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _userAccessor = userAccessor;
        }

        public async Task<User> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByName(_userAccessor.GetCurrentUserName());
            
            return new User
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Token = _jwtGenerator.CreateToken(user),
                Image = null
            };
        }
    }
}
