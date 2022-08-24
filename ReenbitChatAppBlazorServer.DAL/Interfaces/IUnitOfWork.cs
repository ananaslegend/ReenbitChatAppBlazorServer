namespace ReenbitChatAppBlazorServer.DAL.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }
    public IChatRepository Chats { get; }
    public IMessageRepository Messages { get; }
    Task<int> CompleteAsync();
}