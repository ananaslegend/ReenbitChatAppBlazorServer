namespace ReenbitChatAppBlazorServer.PL.Helpers.Intrefaces;

public interface ITokenHelper
{
    public Task<string?> GetTokenAsync();
    
    public Task RemoveTokenAsync();
    
    public Task WriteTokenAsync(HttpResponseMessage response);
}