using Library.Repository.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Library.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var logger = scope.ServiceProvider.GetService<ILogger<LibraryContextSeed>>();
            var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            db.Database.Migrate();

            new LibraryContextSeed()
                     .SeedAsync(db, logger)
                     .Wait();
        }

        host.Run(); 
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
