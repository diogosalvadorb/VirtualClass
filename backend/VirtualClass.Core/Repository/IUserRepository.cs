using VirtualClass.Core.Entities;

namespace VirtualClass.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task CreateUserAsync(User user);
    }
}
