global using Price_Evaluator.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Price_Evaluator.Client.Services;
using Price_Evaluator.Server.Database;
using Price_Evaluator.Server.Services;
using Price_Evaluator.Server.Services.ProductServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IRegisterServices, RegisterService>();
builder.Services.AddScoped<IProductService, ProductService>();
/*builder.Services.AddScoped<IProductClientServices, ProductClientServices>();*/


//if (!builder.Services.Any(x => x.ServiceType == typeof(HttpClient)))
//{
//    builder.Services.AddScoped<HttpClient>(s =>
//    {
//        NavigationManager navman = s.GetRequiredService<NavigationManager>();
//        return new HttpClient
//        {
//            BaseAddress = new Uri(navman.BaseUri)
//        };
//    });
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
