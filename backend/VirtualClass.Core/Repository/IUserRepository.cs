using VirtualClass.Core.Entities;

namespace VirtualClass.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task CreateUserAsync(User user);
    }
}
