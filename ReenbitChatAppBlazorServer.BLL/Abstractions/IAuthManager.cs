using ReenbitChatAppBlazorServer.DAL.Models;
using ReenbitChatAppBlazorServer.Domain;
using ReenbitChatAppBlazorServer.Domain.DTOs;

namespace ReenbitChatAppBlazorServer.BLL.Abstractions;

public interface IAuthManager
{
    public Task<ApplicationUser> Auth(LoginDTO userData);
    public Task<RegistrationState> Registration(LoginDTO userData);
}