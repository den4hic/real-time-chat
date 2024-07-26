using Microsoft.EntityFrameworkCore;
using real_time_chat.Data;
using real_time_chat.Models;

namespace real_time_chat.Services
{
    public class UserService : IUserService
    {
        private readonly RealTimeChatDbContext _context;

        public UserService(RealTimeChatDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _context.Users.Include(u => u.Messages).ToListAsync();
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.Include(u => u.Messages).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> PostUser(User user)
        {
            if (_context.Users.Any(u => u.Name == user.Name))
            {
                var existedUser = await _context.Users.Include(u => u.Messages).FirstOrDefaultAsync(u => u.Name == user.Name);

                if (existedUser != null) return existedUser;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
