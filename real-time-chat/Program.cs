using Microsoft.EntityFrameworkCore;
using real_time_chat.Components;
using real_time_chat.Data;
using real_time_chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<RealTimeChatDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealTimeChatDbConnection"));
});

builder.Services.AddSignalR().AddAzureSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapHub<ChatHub>("/chathub");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
