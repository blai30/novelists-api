using System.Data;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovelistsApi.Infrastructure.Persistence;
using Npgsql;

namespace NovelistsApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        services.AddDbContextFactory<NovelistsDbContext>(builder =>
        {
            builder.UseNpgsql(connectionString);
            // Map PascalCase POCO properties to snake_case MySQL tables and columns.
            builder.UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
