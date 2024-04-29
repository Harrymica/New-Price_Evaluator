//using Blazorise;
//using Blazorise.Tailwind;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Price_Evaluator.Client;
using Price_Evaluator.Client.Services;
using System.Security.Claims;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IProductClientServices, ProductClientServices>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Role", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});
//builder.Services
//    .AddBlazorise()
//    .AddTailwindProviders()
//    .AddFontAwesomeIcons();
//builder.Services.AddBlazorTailwindPack();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
