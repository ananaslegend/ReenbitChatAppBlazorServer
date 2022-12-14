using ReenbitChatAppBlazorServer.Domain.Enums;

namespace ReenbitChatAppBlazorServer.PL.Handlers.Intrefaces;

public interface IAuthHandler
{
    public Task LoginAsync(HttpResponseMessage response, string username);
    public Task LogoutAsync();
    public Task<AuthState> AuthCheckAsync();
}