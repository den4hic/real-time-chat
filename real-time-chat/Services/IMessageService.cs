using real_time_chat.Models;

namespace real_time_chat.Services
{
    public interface IMessageService
    {
        Task<IList<Message>> GetMessages();
        Task<Message?> GetMessage(int id);
        Task<Message> PostMessage(Message message);
        Task<bool> DeleteMessage(int id);
        Task<bool> PutMessage(int id, Message message);
    }
}