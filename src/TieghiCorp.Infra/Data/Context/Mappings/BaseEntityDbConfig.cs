using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Infra.Data.Context.Mappings;

internal static class BaseEntityDbConfig
{
    internal static void BaseEntityConfigure<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
    {
        builder.HasKey(x => x.Id);
    }
}