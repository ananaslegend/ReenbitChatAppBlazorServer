using Microsoft.AspNetCore.Identity;

namespace ReenbitChatAppBlazorServer.DAL.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Chat> UserChats { get; set; }
}