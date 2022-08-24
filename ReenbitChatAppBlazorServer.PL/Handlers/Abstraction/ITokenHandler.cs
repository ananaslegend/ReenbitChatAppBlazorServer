namespace ReenbitChatAppBlazorServer.PL.Handlers.Intrefaces;

public interface ITokenHandler
{
    public Task<string?> GetTokenAsync();
    
    public Task RemoveTokenAsync();
    
    public Task WriteTokenAsync(HttpResponseMessage response);
}