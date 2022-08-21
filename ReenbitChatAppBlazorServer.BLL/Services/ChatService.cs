using Chat_BlazorServer.Controllers;
using Microsoft.AspNetCore.Identity;
using ReenbitChatAppBlazorServer.DB.Interfaces;
using ReenbitChatAppBlazorServer.Domain.DTOs;
using ReenbitChatAppBlazorServer.Domain.Enums;
using ReenbitChatAppBlazorServer.Domain.Models;
using ReenbitChatAppBlazorServer.PL.Models;

namespace ReenbitChatAppBlazorServer.BLL.Services;

public class ChatService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _dbUnit;

    public ChatService(UserManager<ApplicationUser> userManager, IUnitOfWork dbUnit)
    {
        _userManager = userManager;
        _dbUnit = dbUnit;
    }

    public async Task<bool> CreateChatAsync(CreateChatDTO chatModel)
    {
        var newChat = new Chat() { Name = chatModel.ChatName };
        try
        {
            switch (chatModel.Type)
            {
                case ChatState.Private:
                    newChat.Type = ChatType.PrivateChat;
                    var companion = _userManager.Users.FirstOrDefault(x => x.UserName == chatModel.CompanionName);
                    newChat.ChatUsers.Add(companion);
                    break;
                case ChatState.Public:
                    newChat.Type = ChatType.PublicChat;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var user = _userManager.Users.FirstOrDefault(x => x.UserName == chatModel.UserName);
            newChat.ChatUsers.Add(user);

            _dbUnit.Chats.Add(newChat);

            await _dbUnit.CompleteAsync();


            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public IEnumerable<ChatDisplayDTO> GetAllUserChats(NameDTO userName)
    {
        try
        {
            var user = _userManager.FindByNameAsync(userName.Name).Result;

            var list = _dbUnit.Chats.GetAllUserChats(user);

            return list.Select(
                item => new ChatDisplayDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.Type
                }).ToList();
        }
        catch (Exception e)
        {
            return Enumerable.Empty<ChatDisplayDTO>();
        }
       

    }

    public List<ChatDisplayDTO> FindChats(NameDTO chatname)
    {
        var list = _dbUnit.Chats.GetChatsByName(chatname.Name);
        
        return list.Select(item => new ChatDisplayDTO()
        {
            Id = item.Id,
            Name = item.Name,
            Type = item.Type
        }).ToList();
    }

    public async Task<bool> JoinUserToChat(JoinChatDTO model)
    {
        var user = _userManager.FindByNameAsync(model.UserName).Result;
        
        try
        {
            _dbUnit.Chats.AddUserToChat(model.ChatId, user);
            await _dbUnit.CompleteAsync();
        }
        catch(Exception ex)
        {
            return false;
        }

        return true;
    }
}