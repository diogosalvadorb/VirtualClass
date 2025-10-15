using VirtualClass.Core.Entities;

namespace VirtualClass.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<User?> GetUserByEmailConfirmationTokenAsync(string token);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);    
    }
}
