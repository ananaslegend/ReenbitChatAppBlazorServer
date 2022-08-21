using Blazored.LocalStorage;
using ReenbitChatAppBlazorServer.Domain.Enums;
using ReenbitChatAppBlazorServer.PL.Helpers.Intrefaces;

namespace ReenbitChatAppBlazorServer.PL.Helpers;
internal class LocalStorageHandler : ILocalStorageHandler
{
    private readonly ILocalStorageService _localStorage;

    public LocalStorageHandler(ILocalStorageService localStorage)
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

    public async Task LoginAsync(HttpResponseMessage response, string username)
    {
        await _localStorage.SetItemAsync<string>("UserName", username);
        await WriteTokenAsync(response);
    }

    public async Task LogoutAsync()
    {
        await RemoveTokenAsync();
        await _localStorage.RemoveItemAsync("UserName");
    }

    public async Task<AuthState> AuthCheckAsync()
    {
        return (await _localStorage.GetItemAsStringAsync("authToken") != null
                & await _localStorage.GetItemAsStringAsync("UserName") != null)
            ? AuthState.Authed
            : AuthState.NotAuthed;
    }
}