using Microsoft.EntityFrameworkCore;
using real_time_chat.Data;
using real_time_chat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace real_time_chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly RealTimeChatDbContext _context;

        public MessageService(RealTimeChatDbContext context)
        {
            _context = context;
        }
        
        public async Task<IList<Message>> GetMessages()
        {
            return await _context.Messages.Include(m => m.User).ToListAsync();
        }
        
        public async Task<Message?> GetMessage(int id)
        {
            return await _context.Messages.Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<Message> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
        
        public async Task<bool> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return false;
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> PutMessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return false;
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
