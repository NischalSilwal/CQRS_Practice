using CQRS_Practice.DTOs;
using MediatR;

namespace CQRS_Practice.Command
{
    public class LoginCommand : IRequest<string>
    {
        public UserDto LoginDto { get; }

        public LoginCommand(UserDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
