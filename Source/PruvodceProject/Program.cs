using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using PruvodceProject.Interfaces;
using PruvodceProject.Models;

namespace PruvodceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add env interpreter because .NET can not do that by default
            //Takhle dostanete ven data: Environment.GetEnvironmentVariable("EMAIL_UCET").ToString()
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            EnvInterpret.Load(dotenv);

            // Add email service
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Data.PruvodceData>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".pruvodce";
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                nasaditDataBudov.Initialize(services);
                nasaditDataStravovacichZarizeni.Initialize(services);
                nasaditDataUceben.Initialize(services);
                nasaditDataKFotkamUceben.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            }

            //Je pro wwwroot soubor
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Slouží pro možnost ASP.NET rozeznávat .gltf a .glb soubory
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                provider.Mappings[".glb"] = "model/gltf+binary";
                provider.Mappings[".gltf"] = "model/gltf+json";

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/soubor3D")),
                    RequestPath = "/wwwroot/soubor3D",
                    ContentTypeProvider = provider
                });

            app.UseRouting();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}