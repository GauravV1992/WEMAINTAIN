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
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
            services.AddScoped(typeof(IServiceService), typeof(ServiceService));
            services.AddScoped(typeof(ISubPackageRepository), typeof(SubPackageRepository));
            services.AddScoped(typeof(ISubPackageService), typeof(SubPackageService));

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
