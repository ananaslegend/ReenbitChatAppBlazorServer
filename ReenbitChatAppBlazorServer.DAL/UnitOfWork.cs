using ReenbitChatAppBlazorServer.DAL.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Repositories;

namespace ReenbitChatAppBlazorServer.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;
    
    public IUserRepository Users { get; private set; }
    public IChatRepository Chats { get; private set; }
    public IMessageRepository Messages { get; private set; }
    
    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Chats = new ChatRepository(_context);
        Messages = new MessageRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}