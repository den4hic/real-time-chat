using real_time_chat.Models;

namespace real_time_chat.Services
{
    public interface IUserService
    {
        Task<IList<User>> GetUsers();
        Task<User?> GetUser(int id);
        Task<User> PostUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> PutUser(int id, User user);
    }
}