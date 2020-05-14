// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.User.Commands
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;
    using Domain.Repositories;

    using Exceptions;

    using MediatR;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, User>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailExistInDb(request.Email))
                throw new RestException(HttpStatusCode.BadRequest, new {Email = "Email already exist"});
            
            if (await _userRepository.UserNameExistInDb(request.UserName))
                throw new RestException(HttpStatusCode.BadRequest, new {UserName = "Username already exist"});

            var user = new AppUser
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userRepository.Add(user, request.Password);
            
            if (result) return  new User
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Token = _jwtGenerator.CreateToken(user),
                Image = null
            };
            
            throw new Exception("Problem saving changes");
        }
    }
}
