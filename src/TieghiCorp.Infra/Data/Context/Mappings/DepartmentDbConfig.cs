using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TieghiCorp.Core.Entities;

namespace TieghiCorp.Infra.Data.Context.Mappings;

internal sealed class DepartmentDbConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.BaseEntityConfigure();

        builder.ToTable("Department");

        builder
            .Property(d => d.Name)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(d => d.LocationId)
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasIndex(d => d.Name)
            .IsUnique();

        builder
            .HasMany<Personnel>()
            .WithOne(p => p.Department)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Navigation(d => d.Location)
            .AutoInclude();
    }
}