using Microsoft.AspNetCore.Identity;

namespace ReenbitChatAppBlazorServer.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Chat> UserChats { get; set; }
}