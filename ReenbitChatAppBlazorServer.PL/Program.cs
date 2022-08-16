using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ReenbitChatAppBlazorServer.PL;

var builder = CompositionRoot.AddServices(
        WebApplication.CreateBuilder(args));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();