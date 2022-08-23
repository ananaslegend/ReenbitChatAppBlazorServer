using ReenbitChatAppBlazorServer.Domain.Enums;

namespace ReenbitChatAppBlazorServer.PL.Handlers.Abstraction;

public interface IAuthHandler
{
    public Task LoginAsync(HttpResponseMessage response, string username);
    public Task LogoutAsync();
    public Task<AuthState> AuthCheckAsync();
}