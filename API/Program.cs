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
using Repositories.Interface;
using Repositories.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Diagnostics;
using BusinessServices.Interface;
using BusinessServices.Implementation;
using API.JWTMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        //x.Events = new JwtBearerEvents
        //{
        //    OnMessageReceived = context =>
        //    {
        //        var token = context.HttpContext.Request.Cookies["access_token"];
        //        context.Token = token;
        //        return Task.CompletedTask;
        //    },

        //};
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
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






var app = builder.Build();
//app.UseRouting();

ServicesConfig.AddConfigure(app, builder.Environment);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JWTMiddleware>();


app.MapControllers();

app.Run();

