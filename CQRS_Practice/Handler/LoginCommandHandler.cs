using CQRS_Practice.Command;
using CQRS_Practice.Repository;
using CQRS_Practice.Utility;
using MediatR;


namespace CQRS_Practice.Handler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public LoginCommandHandler(IUserRepository userRepository, JwtTokenHelper jwtTokenHelper)
        {
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Authenticate(request.LoginDto.Username, request.LoginDto.Password);
            if (user == null)
            {
                return null; // Unauthorized
            }

            return _jwtTokenHelper.GenerateToken(user);
        }
    }
}
