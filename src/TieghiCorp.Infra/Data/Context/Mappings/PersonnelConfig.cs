using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TieghiCorp.Core.Entities;

namespace TieghiCorp.Infra.Data.Context.Mappings;

internal sealed class PersonnelConfig : IEntityTypeConfiguration<Personnel>
{
    public void Configure(EntityTypeBuilder<Personnel> builder)
    {
        builder
            .BaseEntityMap();

        builder
            .ToTable("Personnel");

        builder
            .Property(p => p.FirstName)
            .HasColumnType("varchar")
            .HasMaxLength(75)
            .IsRequired();

        builder
            .Property(p => p.LastName)
            .HasColumnType("varchar")
            .HasMaxLength(75)
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(p => p.JobTitle)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(p => p.DepartmentId)
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasIndex(p => p.Email)
            .IsUnique();

        builder
            .Navigation(p => p.Department)
            .AutoInclude();
    }
}