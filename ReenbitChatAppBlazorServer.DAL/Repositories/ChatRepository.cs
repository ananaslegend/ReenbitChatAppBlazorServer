using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DB.Interfaces;
using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.DB.Repositories;

internal class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    public ChatRepository(ApplicationContext context) : base(context)
    {
    }

    //downcast from generic 
    public ApplicationContext ApplicationContext => _context;

    public IEnumerable<Chat> GetChatsByName(string chatName)
    {
        return ApplicationContext.Chats.Where(c => c.Name == chatName).AsEnumerable<Chat>();
    }

    public IEnumerable<Chat> GetAllUserChats(ApplicationUser user)
    {
        return ApplicationContext.Chats.Where(u => u.ChatUsers.Contains(user));
    }

    public void AddUserToChat(int chatId, ApplicationUser user)
    {
        var chat = ApplicationContext.Chats
            .Include(u => u.ChatUsers)
            .FirstOrDefault(c => c.Id == chatId);
        if (chat is null)
        {
            throw new Exception("User not found");
        }

        if(!chat.ChatUsers.Contains(user))
        {
            chat.ChatUsers.Add(user);
        }
    }
}