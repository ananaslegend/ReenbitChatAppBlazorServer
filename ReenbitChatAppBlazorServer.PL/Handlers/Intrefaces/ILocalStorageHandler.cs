namespace ReenbitChatAppBlazorServer.PL.Handlers.Intrefaces;

public interface ILocalStorageHandler : ITokenHandler, IAuthHandler
{
    public Task<string?> GetUserNameAsync();
}