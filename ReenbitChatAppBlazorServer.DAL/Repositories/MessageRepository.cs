using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DAL.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Models;

namespace ReenbitChatAppBlazorServer.DAL.Repositories;

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

    public async Task<Message> GetMessageByIdAsync(int Id)
    {
        return await ApplicationContext.Messages
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