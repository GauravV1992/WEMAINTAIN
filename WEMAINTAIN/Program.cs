
using Microsoft.Net.Http.Headers;
using System.Net;
using WEMAINTAIN.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient("WEMAINTAIN", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.fixmyspace.org/api/");
    //httpClient.BaseAddress = new Uri("https://localhost:7019/api/");
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.Accept, "application/vnd.github.v3+json");
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.UserAgent, "HttpRequestsSample");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "client",
   pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
