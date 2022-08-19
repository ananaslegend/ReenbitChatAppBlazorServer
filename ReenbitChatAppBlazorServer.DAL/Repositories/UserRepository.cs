using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DB.Interfaces;
using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.DB.Repositories;

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

    //downcast from generic 
    public ApplicationContext ApplicationContext => _context;
}