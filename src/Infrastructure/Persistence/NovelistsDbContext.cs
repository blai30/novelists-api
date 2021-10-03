using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Persistence;

// TODO: Change DbContext to ApiAuthorizationDbContext<ApplicationUser> when adding authorization.
public class NovelistsDbContext : DbContext
{
    public NovelistsDbContext(DbContextOptions<NovelistsDbContext> options) :
        base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Publication> Publications { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("novelists");
        modelBuilder.HasPostgresExtension("uuid-ossp");
        // Load entity type configuration mappers.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
