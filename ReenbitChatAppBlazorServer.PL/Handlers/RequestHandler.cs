using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ReenbitChatAppBlazorServer.PL.Handlers.Intrefaces;
using ReenbitChatAppBlazorServer.PL.Models;

namespace ReenbitChatAppBlazorServer.PL.Handlers;

public class RequestHandler
{
    private readonly ILocalStorageHandler _localStorageHelper;
    private readonly HttpClient _client;

    public RequestHandler(IHttpClientFactory httpClientFactory, ILocalStorageHandler localStorageHelper)
    {
        _localStorageHelper = localStorageHelper;

        _client = httpClientFactory.CreateClient("BaseClient");
    }
    
    public async Task<HttpResponseMessage> GetAuthAsync(string path)
    {
        var token = await _localStorageHelper.GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _client.GetAsync(path);
    }

    public async Task<HttpResponseMessage> CreateChat(CreateChatDTO chat)
    {
        var token = await _localStorageHelper.GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var json = JsonConvert.SerializeObject(chat);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");
        
        return await _client.PostAsync("chats/create_chat", payload);
    }

    //todo GetAuthRequest()
}