using CatchTrackerApi.Data;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatchTrackerApi.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly FishingDbContext _context;

        public UserRepository(FishingDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteUserAsync(int id)
        {
            var deleting = await GetUserByIdAsync(id);
            if (deleting == null)
            {
                return null;
            }
            _context.Users.Remove(deleting);
            await _context.SaveChangesAsync();
            return deleting;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
