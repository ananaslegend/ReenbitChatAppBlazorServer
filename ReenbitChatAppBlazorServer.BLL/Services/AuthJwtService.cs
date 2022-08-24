using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReenbitChatAppBlazorServer.BLL.Services.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Models;
using ReenbitChatAppBlazorServer.Domain;
using ReenbitChatAppBlazorServer.Domain.DTOs;

namespace ReenbitChatAppBlazorServer.BLL.Services;

public class AuthJwtService : IAuthJwtService
{
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthJwtService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        
        public async Task<ApplicationUser> Auth(LoginDTO userData)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == userData.UserName);

            if (user != null)
            {
                return await _userManager.CheckPasswordAsync(user, userData.Password)
                    ? user : throw new Exception("Incorrect password");
            }

            throw new Exception("User not found");
        }
        
        public async Task<RegistrationState> Registration(LoginDTO userData)
        {
            var existUser = _userManager.Users.FirstOrDefault(x => x.UserName == userData.UserName);

            if (existUser != null)
                return new RegistrationState() { UserNameIsAlreadyExist = true};
            
            var user = new ApplicationUser { UserName = userData.UserName };
            var result = await _userManager.CreateAsync(user, userData.Password);

            return new RegistrationState() 
                { Succeeded = result.Succeeded, Errors = result.Errors };
        }
        
        public string GenerateJwtToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }