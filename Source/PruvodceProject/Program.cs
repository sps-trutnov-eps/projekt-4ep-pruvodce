using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace PruvodceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Je pro wwwroot soubor
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}