﻿using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.DB.Interfaces;

public interface IMessageRepository : IGenericRepository<Message>
{
    public Task<ICollection<Message>> GetMessagePack(int chatId, int loaded, int batch);
    public Task<Message> GetMessageById(int Id);
    public void UpdateMessageData(Message message, string data);
}