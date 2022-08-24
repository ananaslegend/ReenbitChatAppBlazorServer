using Chat_BlazorServer.Controllers;
using ReenbitChatAppBlazorServer.DAL.Models;
using ReenbitChatAppBlazorServer.Domain.DTOs;
using ReenbitChatAppBlazorServer.Domain.Enums;
using ReenbitChatAppBlazorServer.PL.Models;

namespace ReenbitChatAppBlazorServer.BLL.Services.Interfaces;

public interface IChatService
{
    public Task<bool> CreateChatAsync(CreateChatDTO chatModel);

    public Task<IEnumerable<ChatDisplayDTO>> GetAllUserChats(NameDTO userName);

    public List<ChatDisplayDTO> FindChats(NameDTO chatname);

    public Task<bool> JoinUserToChat(JoinChatDTO model);

}
