using ReenbitChatAppBlazorServer.DAL.Models;

namespace ReenbitChatAppBlazorServer.DAL.Interfaces;

public interface IUserRepository : IGenericRepository<ApplicationUser>
{
    public Task<ICollection<Chat>> GetUserChats(string userName);
}