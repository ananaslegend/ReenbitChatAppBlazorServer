using ReenbitChatAppBlazorServer.Domain.Enums;

namespace ReenbitChatAppBlazorServer.Domain.DTOs;

public class ChatDisplayDTO
{
    public int Id { get; set; } // todo
    public string Name { get; set; }
    public ChatType Type { get; set; }
}