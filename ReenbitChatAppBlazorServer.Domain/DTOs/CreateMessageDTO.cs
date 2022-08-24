namespace ReenbitChatAppBlazorServer.Domain.DTOs;

public class CreateMessageDTO
{
    public int ChatId { get; set; }
    public string MessageText { get; set; }
    public string? SenderName { get; set; }
    public int? ReplyId { get; set; }
}