using Microsoft.Net.Http.Headers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient("WEMAINTAIN", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7019/api/");
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.Accept, "application/vnd.github.v3+json");
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.UserAgent, "HttpRequestsSample");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
