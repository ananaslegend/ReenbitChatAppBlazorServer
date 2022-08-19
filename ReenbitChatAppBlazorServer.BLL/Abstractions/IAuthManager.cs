using ReenbitChatAppBlazorServer.Domain.DTOs;
using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.BLL.Abstractions;

public interface IAuthManager
{
    public Task<ApplicationUser> Auth(LoginDTO userData);
    public Task<RegistrationState> Registration(LoginDTO userData);
}