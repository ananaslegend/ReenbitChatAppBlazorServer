using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.BLL.Abstractions;

public interface IJwtTokenManager
{
    public string GenerateJwtToken(ApplicationUser user);
}