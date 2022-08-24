using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DAL.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Models;

namespace ReenbitChatAppBlazorServer.DAL.Repositories;

internal class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    public ChatRepository(ApplicationContext context) : base(context)
    {
    }

    //downcast from generic 
    public ApplicationContext ApplicationContext => _context;

    public IEnumerable<Chat> GetChatsByName(string? chatName)
    {
        return ApplicationContext.Chats.Where(c => c.Name == chatName).AsEnumerable(); //todo
    }

    public IEnumerable<Chat> GetAllUserChats(ApplicationUser user)
    {
        return ApplicationContext.Chats.Where(u => u.ChatUsers.Contains(user)); //todo
    }

    public void AddUserToChat(int chatId, ApplicationUser user)
    {
        var chat = ApplicationContext.Chats
            .Include(u => u.ChatUsers)
            .FirstOrDefault(c => c.Id == chatId); //todo
        
        if (chat == null)
        {
            throw new Exception("User not found");
        }

        if(!chat.ChatUsers.Contains(user))
        {
            chat.ChatUsers.Add(user);
        }
    }
}