using Microsoft.EntityFrameworkCore;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;

namespace VirtualClass.Infrastructure.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly VirtualClassDbContext _context;
        public UserRepository(VirtualClassDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }
    }
}
