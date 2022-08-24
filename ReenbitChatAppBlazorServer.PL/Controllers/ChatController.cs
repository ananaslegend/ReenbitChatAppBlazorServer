using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReenbitChatAppBlazorServer.BLL.Services.Interfaces;
using ReenbitChatAppBlazorServer.Domain.DTOs;
using ReenbitChatAppBlazorServer.PL.Models;

namespace Chat_BlazorServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }
        
        [HttpPost("all_user_chats/")]
        public async Task<IActionResult> GetAllUserChats([FromBody] NameDTO userName)
        {
            return Ok(_chatService.GetAllUserChats(userName));
        }

        [HttpPost("find_chats/")]
        public async Task<IActionResult> FindChats([FromBody] NameDTO chatname)
        {
            return Ok(_chatService.FindChats(chatname));
        }
        
        [HttpPost("create_chat")]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDTO chatModel)
        {
            return await _chatService.CreateChatAsync(chatModel) ? Ok() : BadRequest();
        }

        [HttpPost("join_to_chat/")]
        public async Task<IActionResult> JoinToChat([FromBody] JoinChatDTO model)
        {
            return await _chatService.JoinUserToChat(model) ? Ok() : BadRequest();
        }
    }
}