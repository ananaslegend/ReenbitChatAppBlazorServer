using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReenbitChatAppBlazorServer.DB;
using ReenbitChatAppBlazorServer.Domain.Models;

namespace ReenbitChatAppBlazorServer.PL;

public static class CompositionRoot
{
    public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        
        builder.Services.AddDbContext<ApplicationContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"],
                b => b.MigrationsAssembly("ReenbitChatAppBlazorServer.DB"));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 5;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationContext>();

        return builder;
    }
}