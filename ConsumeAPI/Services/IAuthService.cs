using ConsumeAPI.Models;

namespace ConsumeAPI.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(UserLogin loginModel);
    }
}
