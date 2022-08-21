using ReenbitChatAppBlazorServer.Domain.Enums;

namespace ReenbitChatAppBlazorServer.PL.Models;

public class CreateChatDTO
{
    public string ChatName { get; set; }
    public string UserName { get; set; }
    public string? CompanionName { get; set; }
    public ChatState Type { get; set; }
}