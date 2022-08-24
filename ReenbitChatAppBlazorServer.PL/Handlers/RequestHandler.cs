using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Chat_BlazorServer.Controllers;
using Newtonsoft.Json;
using ReenbitChatAppBlazorServer.Domain.DTOs;
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

    private async Task GetAuthClient()
    {
        var token = await _localStorageHelper.GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
    
    public async Task<HttpResponseMessage> GetAuthAsync(string path)
    {
        await GetAuthClient();
        
        return await _client.GetAsync(path);
    }

    public async Task<HttpResponseMessage> CreateChat(CreateChatDTO chat)
    {
        await GetAuthClient();

        var json = JsonConvert.SerializeObject(chat);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");
        
        return await _client.PostAsync("chats/create_chat", payload);
    }

    public async Task<HttpResponseMessage> GetAllUserChats(NameDTO nameDto)
    {
        await GetAuthClient();

        var json = JsonConvert.SerializeObject(nameDto);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("chats/all_user_chats/", payload);
        return response;
    }

    public async Task<HttpResponseMessage> GetChatsByName(NameDTO chatName)
    {
        await GetAuthClient();
    
        var json = JsonConvert.SerializeObject(chatName);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");
    
        return await _client.PostAsync("chats/find_chats/", payload);
    }

    public async Task<bool> JoinToChat(int chatId, string userName)
    {
        await GetAuthClient();
        
        var json = JsonConvert.SerializeObject(new JoinChatDTO()
        {   
            ChatId = chatId,
            UserName = userName
        });
        
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("chats/join_to_chat/", payload);

        return response.IsSuccessStatusCode;
    }
}