using ReenbitChatAppBlazorServer.DAL.Models;

namespace ReenbitChatAppBlazorServer.BLL.Abstractions;

public interface IJwtTokenManager
{
    public string GenerateJwtToken(ApplicationUser user);
}