using System.Net.Http.Headers;
using ReenbitChatAppBlazorServer.PL.Helpers.Intrefaces;

namespace ReenbitChatAppBlazorServer.PL.Helpers;

public class RequestHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenHelper _tokenHelper;
    private readonly HttpClient _client;

    public RequestHelper(IHttpClientFactory httpClientFactory, ITokenHelper tokenHelper)
    {
        _httpClientFactory = httpClientFactory;
        _tokenHelper = tokenHelper;

        _client = httpClientFactory.CreateClient("BaseClient");
    }
    
    public async Task<HttpResponseMessage> GetAuthAsync(string path)
    {
        var token = await _tokenHelper.GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _client.GetAsync(path);
    }
}