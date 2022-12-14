using Microsoft.AspNetCore.Identity;

namespace ReenbitChatAppBlazorServer.Domain;

public class RegistrationState
{
    public bool Succeeded { get; set; }
    public bool UserNameIsAlreadyExist { get; set; }
    public IEnumerable<IdentityError> Errors { get; set; }
}