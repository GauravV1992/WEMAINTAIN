using BusinessServices.Implementation;
using BusinessServices.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories.Implementation;
using Repositories.Interface;


namespace API.Helpers
{
    public static class ServicesConfig
    {
        public static void AddExtensionServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddSingleton(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
            services.AddScoped(typeof(IServiceService), typeof(ServiceService));
            services.AddScoped(typeof(ISubPackageRepository), typeof(SubPackageRepository));
            services.AddScoped(typeof(ISubPackageService), typeof(SubPackageService));

            services.AddScoped(typeof(IPackageRateRepository), typeof(PackageRateRepository));
            services.AddScoped(typeof(IPackageRateService), typeof(PackageRateService));

            services.AddScoped(typeof(IPackageRateLogRepository), typeof(PackageRateLogRepository));
            services.AddScoped(typeof(IPackageRateLogService), typeof(PackageRateLogService));

            services.AddScoped(typeof(IPurchaseDetailsRepository), typeof(PurchaseDetailsRepository));
            services.AddScoped(typeof(IPurchaseDetailsService), typeof(PurchaseDetailsService));
            services.AddSingleton(typeof(IJWTAuthenticaitonManagerRepository), typeof(JWTAuthenticaitonManagerRepository));

            services.AddSingleton(typeof (IJWTAuthenticaitonManagerService),typeof(JWTAuthenticaitonManagerService));
            //services.AddScoped<API.JWTMiddleware.JWTMiddleware>();
            services.AddScoped(typeof(IUserService), typeof(UserService));

        }
        public static void AddConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEMAINTAIN v1"));
            }

            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Images")),
            //    RequestPath = new PathString("/Images")
            //});
        }
    }
}
