using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Maps
{
    public class UsersMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Let database generate these values.
            builder.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
            builder.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }
    }
}
