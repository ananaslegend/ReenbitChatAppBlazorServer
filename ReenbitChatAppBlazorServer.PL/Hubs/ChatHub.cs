using Microsoft.AspNetCore.SignalR;
using ReenbitChatAppBlazorServer.BLL.Services;
using ReenbitChatAppBlazorServer.BLL.Services.Interfaces;
using ReenbitChatAppBlazorServer.Domain.DTOs;

namespace ReenbitChatAppBlazorServer.PL.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;
    
    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }
        
    public async Task JoinRoom(int chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Convert.ToString(chatId));
    }
    
    public async Task SendMessage(CreateMessageDTO createMessage)
    {
        var newMessageItem = await _messageService.AddNewMessageAsync(createMessage);
        
        await Clients.Group(createMessage.ChatId.ToString())
            .SendAsync("AddMessage", newMessageItem);
    }
    
    public async Task GetMassagePack(int chatId, int loaded, int batch)
    {
        var pack = await _messageService.GetMessagePack(chatId, loaded, batch);
        await Clients.Caller.SendAsync("AddMessagePack", pack);
    }
    
    public async Task RemoveMessage(MessageDTO message)
    {
        try
        {
            await _messageService.RemoveAsync(message.Id);
            
            await Clients.Group(message.ChatId.ToString())
                .SendAsync("ReceiveDeleteMessage", message.Id);
        }
        catch(Exception ex)
        {
            return;
        }
    }
    
    public async Task UpdateMessage(MessageDTO message)
    {
        await _messageService.UpdateAsync(message);
    
        await Clients.Group(message.ChatId.ToString())
            .SendAsync("ReceiveUpdateMessage", message);
    }
}