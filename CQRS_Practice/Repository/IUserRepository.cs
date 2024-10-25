using CQRS_Practice.Model;

namespace CQRS_Practice.Repository
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
        Task RegisterUserAsync(User user);
    }
}
