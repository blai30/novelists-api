using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDbContextFactory<NovelistsDbContext>();

            return services;
        }
    }
}
