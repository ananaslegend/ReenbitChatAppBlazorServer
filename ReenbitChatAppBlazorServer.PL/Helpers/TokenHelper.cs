using Blazored.LocalStorage;
using ReenbitChatAppBlazorServer.PL.Helpers.Intrefaces;

namespace ReenbitChatAppBlazorServer.PL.Helpers;
internal class TokenHelper : ITokenHelper
{
    private readonly ILocalStorageService _localStorage;

    public TokenHelper(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
        
    public async Task<string?> GetTokenAsync()
    {
        var authToken = await _localStorage.GetItemAsStringAsync("authToken");

        return authToken?.Trim('"');
    }

    public async Task RemoveTokenAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
    }

    public async Task WriteTokenAsync(HttpResponseMessage response)
    {
        var token = await response.Content.ReadAsStringAsync();

        await _localStorage.SetItemAsync<string>("authToken", token);
    }
}