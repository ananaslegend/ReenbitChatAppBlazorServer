using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DAL.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Models;

namespace ReenbitChatAppBlazorServer.DAL.Repositories;

internal class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {

    }

    public async Task<ApplicationUser> FindUser(string userName)
    {
        var user = await ApplicationContext
            .Users
            .Where(u => u.UserName == userName)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<ICollection<Chat>> GetUserChats(string userName)
    {
        var user = await ApplicationContext.Users
            .Where(u => u.UserName == userName)
            .Include(c => c.UserChats)
            .FirstOrDefaultAsync();

        return user.UserChats.ToList();
    }

    //downcast from generic 
    public ApplicationContext ApplicationContext => _context;
}