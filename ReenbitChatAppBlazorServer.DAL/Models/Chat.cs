using ReenbitChatAppBlazorServer.Domain.Enums;

namespace ReenbitChatAppBlazorServer.DAL.Models;

public class Chat
{
    public int Id { get; set; }
    public ChatType Type { get; set; }
    public string? Name { get; set; }
    public ICollection<ApplicationUser> ChatUsers { get; set; } = new List<ApplicationUser>();
    public ICollection<Message>? Messages { get; set; }
}