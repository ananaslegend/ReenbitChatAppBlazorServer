namespace ReenbitChatAppBlazorServer.Domain.DTOs;

public class MessageDTO
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public int ChatId { get; set; }
    public string ChatName { get; set; }
    public DateTime Date { get; set; }
    public string Data { get; set; }
    public string? ProfilePicUrl { get; set; }
    public int? ReplyId { get; set; }
    public string? ReplyAuthorName { get; set; }
    public string? ReplyData { get; set; }
}