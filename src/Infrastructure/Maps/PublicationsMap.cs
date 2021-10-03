using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Maps;

public class PublicationsMap : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        // Let database generate these values.
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    }
}