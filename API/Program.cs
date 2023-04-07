using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using API.Helpers;
using System.IO;
using System.Text;
using Microsoft.OpenApi.Models;
using AutoMapper.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ApplicationDBContext>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WEMAINTAIN", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
//builder.Services.AddAutoMapper();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new BusinessServices.Automapper.AutoMapper());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
ServicesConfig.AddExtensionServices(builder.Services);

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);



var app = builder.Build();
//app.UseRouting();
ServicesConfig.AddConfigure(app, builder.Environment);
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();

