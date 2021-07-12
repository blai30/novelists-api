using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Persistence
{
    // TODO: Change DbContext to ApiAuthorizationDbContext<ApplicationUser> when adding authorization.
    public class NovelistsDbContext : DbContext
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public NovelistsDbContext(DbContextOptions<NovelistsDbContext> options, IServiceScopeFactory scopeFactory) :
            base(options)
        {
            _scopeFactory = scopeFactory;
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Publication> Publications { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            using var scope = _scopeFactory.CreateScope();
            string connectionString = scope.ServiceProvider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("Docker");

            optionsBuilder.UseNpgsql(connectionString);
            // Map PascalCase POCO properties to snake_case MySQL tables and columns.
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("novelists");
            modelBuilder.HasPostgresExtension("uuid-ossp");
            // Load entity type configuration mappers.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
