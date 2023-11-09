using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities.PostAggregate;
using SocialMedia.Infrastructure.Data.Extensions;
namespace SocialMedia.Infrastructure.Data.Context;

public class WriteDbContext : DbContext
{
    private const string Collation = "Latin1_General_CI_AI";

    public WriteDbContext(DbContextOptions<WriteDbContext> dbOptions) : base(dbOptions)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Post> Posts => Set<Post>();

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //modelBuilder.UseCollation(Collation);
    //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    //    modelBuilder.RemoveCascadeDeleteConvention();
    //}
}