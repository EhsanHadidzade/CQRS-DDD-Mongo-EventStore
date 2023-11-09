using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Abstractions;

namespace SocialMedia.Infrastructure.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntity
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .IsRequired()
            .ValueGeneratedNever(); 

        builder.Ignore(entity => entity.DomainEvents);
    }
}