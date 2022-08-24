using ReenbitChatAppBlazorServer.BLL.Services.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Models;
using ReenbitChatAppBlazorServer.Domain.DTOs;

namespace ReenbitChatAppBlazorServer.BLL.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork dbUnit;
    
    public MessageService(IUnitOfWork dbUnit)
    {
        this.dbUnit = dbUnit;
    }
    
    public async Task<MessageDTO> AddNewMessageAsync(CreateMessageDTO createMessage)
    {
        Message msg = new()
        {
            Author = await dbUnit.Users.FindUser(createMessage.SenderName) ?? throw new Exception("User not found"),
            Chat = await dbUnit.Chats.Get(createMessage.ChatId) ?? throw new Exception("Chat not found"),
            Data = createMessage.MessageText,
            Date = DateTime.Now,
        };
        
        if(createMessage.ReplyId != null && createMessage.ReplyId != 0)
        {
            msg.Reply = await dbUnit.Messages.GetMessageByIdAsync(createMessage.ReplyId.Value) ?? throw new Exception("Message not found");
        }
        
        dbUnit.Messages.Add(msg);
        await dbUnit.CompleteAsync();

        MessageDTO messageItem = new()
        {
            Id = msg.Id,
            AuthorName = msg.Author.UserName,
            ChatName = msg.Chat.Name,
            ChatId = msg.Chat.Id,
            Data = msg.Data,
            Date = msg.Date
        };
        
        if (msg.Reply == null) return messageItem;
        
        messageItem.ReplyId = msg.Reply.Id;
        messageItem.ReplyData = msg.Reply.Data;
        messageItem.ReplyAuthorName = msg.Reply.Author.UserName;

        return messageItem;
    }

    public async Task RemoveAsync(int messageId)
    {
        var messageToRemove = await dbUnit.Messages.GetMessageByIdAsync(messageId);

        dbUnit.Messages.Remove(messageToRemove);

        await dbUnit.CompleteAsync();
    }
    
    public async Task UpdateAsync(MessageDTO message)
    {
        var msgToUpdate = await dbUnit.Messages.GetMessageByIdAsync(message.Id);

        dbUnit.Messages.UpdateMessageData(msgToUpdate, message.Data);

        await dbUnit.CompleteAsync();
    }
    
    public async Task<ICollection<MessageDTO>> GetMessagePack(int chatId, int loaded, int batch)
    {
        var messagePack = await dbUnit.Messages.GetMessagePack(chatId, loaded, batch);

        return messagePack.Select(msg => MessageToMessageItem(msg)).ToList();
    }
    
    private MessageDTO MessageToMessageItem(Message msg)
    {
        var messageItem = new MessageDTO()
        {
            Id = msg.Id,
            AuthorName = msg.Author.UserName,
            ChatName = msg.Chat.Name,
            ChatId = msg.Chat.Id,
            Data = msg.Data,
            Date = msg.Date
        };

        if (msg.Reply == null) return messageItem;
        
        messageItem.ReplyId = msg.Reply.Id;
        messageItem.ReplyData = msg.Reply.Data;
        messageItem.ReplyAuthorName = msg.Reply.Author.UserName;

        return messageItem;
    } 
}