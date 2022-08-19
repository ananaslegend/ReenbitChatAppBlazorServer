using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.DB.Interfaces;

public interface IChatRepository : IGenericRepository<Chat>
{
    public IEnumerable<Chat> GetChatsByName(string chatName);
    public void AddUserToChat(int chatId, ApplicationUser user);
    public IEnumerable<Chat> GetAllUserChats(ApplicationUser user);
}