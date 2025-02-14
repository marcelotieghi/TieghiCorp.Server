using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TieghiCorp.Core.Entities;

namespace TieghiCorp.Infra.Data.Context.Mappings;

internal sealed class LocationConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.BaseEntityMap();

        builder.ToTable("Location");

        builder
            .Property(e => e.Name)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasMany<Department>()
            .WithOne(d => d.Location)
            .HasForeignKey(d => d.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}