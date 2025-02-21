using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TieghiCorp.Core.Entities;

namespace TieghiCorp.Infra.Data.Context.Mappings;

internal sealed class LocationDbConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.BaseEntityConfigure();

        builder.ToTable("Location");

        builder
            .Property(l => l.Name)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasIndex(l => l.Name)
            .IsUnique();

        builder
            .HasMany<Department>()
            .WithOne(d => d.Location)
            .HasForeignKey(d => d.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}