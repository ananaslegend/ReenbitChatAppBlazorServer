using ReenbitChatAppBlazorServer.Domain.DTOs;

namespace ReenbitChatAppBlazorServer.BLL.Services.Interfaces;

public interface IMessageService
{
    public Task<MessageDTO> AddNewMessageAsync(CreateMessageDTO createMessage);
    public Task RemoveAsync(int messageId);
    public Task UpdateAsync(MessageDTO message);
    public Task<ICollection<MessageDTO>> GetMessagePack(int chatId, int loaded, int batch);
}