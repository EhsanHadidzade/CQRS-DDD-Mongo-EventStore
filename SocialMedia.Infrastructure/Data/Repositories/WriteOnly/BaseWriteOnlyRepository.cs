using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Interfaces;
using SocialMedia.Infrastructure.Data.Context;
using SocialMedia.Core.Abstractions;

namespace SocialMedia.Infrastructure.Data.Repositories.WriteOnly;

public abstract class BaseWriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;

    protected BaseWriteOnlyRepository(WriteDbContext context)
        => DbSet = context.Set<TEntity>();

    public void Add(TEntity entity)
        => DbSet.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities)
        => DbSet.AddRange(entities);

    public void Update(TEntity entity)
        => DbSet.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities)
        => DbSet.UpdateRange(entities);

    public void Remove(TEntity entity)
        => DbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities)
        => DbSet.RemoveRange(entities);

    public void SaveChangesAsync() => SaveChangesAsync();
   
}