using Library.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.Configurations;

public static class SqlConfiguration
{
    public static IServiceCollection AddSqlData(this IServiceCollection services, IConfiguration configuration)
    {
        services
                .AddDbContext<LibraryContext>(options =>
                {
                    options.UseSqlServer(
                        configuration["ConnectionStrings:DefaultConnection"],
                        o =>
                        {
                            o.MigrationsHistoryTable(tableName: "__ApplicationMigrationsHistory");
                            o.CommandTimeout(30);
                        });
                });

        return services;
    }
}
