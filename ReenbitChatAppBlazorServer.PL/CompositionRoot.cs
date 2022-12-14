using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReenbitChatAppBlazorServer.BLL.Services;
using ReenbitChatAppBlazorServer.BLL.Services.Interfaces;
using ReenbitChatAppBlazorServer.DAL;
using ReenbitChatAppBlazorServer.DAL.Interfaces;
using ReenbitChatAppBlazorServer.DAL.Models;
using ReenbitChatAppBlazorServer.PL.Handlers;
using ReenbitChatAppBlazorServer.PL.Handlers.Intrefaces;

namespace ReenbitChatAppBlazorServer.PL;

public static class CompositionRoot
{
    public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddTransient<ILocalStorageHandler, LocalStorageHandler>();
        builder.Services.AddHttpClient("BaseClient", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["Url:BaseUrl"]);
        });

        builder.Services.AddTransient<RequestHandler>();
        
        builder.Services.AddControllers();

        builder.Services.AddDbContext<ApplicationContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"],
                b => b.MigrationsAssembly("ReenbitChatAppBlazorServer.DAL"));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
        {
            opt.Password.RequireDigit = true;
            opt.Password.RequiredLength = 5;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationContext>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        builder.Services.AddScoped<IAuthJwtService, AuthJwtService>();
        builder.Services.AddTransient<IChatService, ChatService>();
        builder.Services.AddTransient<IMessageService, MessageService>();
        
        builder.Services.AddAuthorization()
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });
        
        builder.Services.AddSignalR();
        
        builder.Services.AddResponseCompression(opt =>
        {
            opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
        });
        
        return builder;
    }
}