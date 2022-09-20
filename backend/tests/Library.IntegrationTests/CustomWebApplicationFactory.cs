using Library.Api;
using Library.Repository.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Library.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LibraryContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<LibraryContext>(options =>
            options.UseInMemoryDatabase("TestDB"));

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var appDb = scopedServices.GetRequiredService<LibraryContext>();

            var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();
            appDb.Database.EnsureCreated();

            try
            {
                 LibraryContextTestSeed.SeedAsync(appDb).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database with test messages. Error: {ex.Message}", ex.Message);
            }
        });
    }
}
