using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DB.Interfaces;
using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.DB.Repositories;

internal class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationContext context) : base(context)
    {
    }
    
    public async Task<ICollection<Message>> GetMessagePack(int chatId, int loaded, int batch)
    {
        var arr = ApplicationContext.Messages
            .Include(a => a.Author)
            .Include(r => r.Reply)
            .Include(c => c.Chat)
            .Where(c => c.Chat.Id == chatId)
            .OrderByDescending(d => d.Date)
            .Skip(loaded)
            .Take(batch);
        var list = arr.ToList();
        list.Reverse();

        return list;
    }

    public Task<Message> GetMessageById(int Id)
    {
        return ApplicationContext.Messages
            .Include(a => a.Author)
            .Include(r => r.Reply)
            .Include(c => c.Chat)
            .FirstAsync(a => a.Id == Id);
    }

    public void UpdateMessageData(Message message, string data)
    {
        message.Data = data;
    }

    //downcast from generic 
    public ApplicationContext ApplicationContext => _context;
}