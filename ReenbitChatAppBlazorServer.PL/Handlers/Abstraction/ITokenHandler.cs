namespace ReenbitChatAppBlazorServer.PL.Handlers.Abstraction;

public interface ITokenHandler
{
    public Task<string?> GetTokenAsync();
    
    public Task RemoveTokenAsync();
    
    public Task WriteTokenAsync(HttpResponseMessage response);
}