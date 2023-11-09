using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using SocialMedia.Core.Events;
using SocialMedia.Infrastructure.Data.Context;
using SocialMedia.Core.Interfaces;
using SocialMedia.Domain.Interfaces.ReadOnly;
using SocialMedia.Domain.Interfaces.WriteOnly;
using SocialMedia.Infrastructure.Behaviors;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Data.Cache;
using SocialMedia.Infrastructure.Data.Repositories;
using SocialMedia.Infrastructure.Data.Repositories.ReadOnly;
using SocialMedia.Infrastructure.Data.Repositories.WriteOnly;

namespace SocialMedia.Infrastructure;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
        => services.AddScoped<ICacheService, MemoryCacheService>();

    public static IServiceCollection AddDistributedCacheService(this IServiceCollection services)
        => services.AddScoped<ICacheService, DistributedCacheService>();

    public static void AddInfrastructure(this IServiceCollection services)
    {
        // MediatR Pipelines
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        // Repositories
        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<IPostWriteOnlyRepository, PostWriteOnlyRepository>();
        services.AddScoped<IPostReadOnlyRepository, PostReadOnlyRepository>();

        // Database Contexts
        services.AddScoped<WriteDbContext>();
        services.AddScoped<ReadDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISyncDataBase, NoSqlSyncDataBase>();

        ConfigureMongoDB();
    }

    private static void ConfigureMongoDB()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

        ConventionRegistry.Register("Conventions", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new EnumRepresentationConvention(BsonType.String),
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfNullConvention(true)
        }, _ => true);


        ApplyMongoDbMappingsFromAssembly();
    }

    private static void ApplyMongoDbMappingsFromAssembly()
    {
        var implementations = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IReadDbMapping).IsAssignableFrom(type)
                && type.IsClass
                && !type.IsAbstract
                && !type.IsInterface)
            .ToList();

        foreach (var impl in implementations)
        {
            var mapping = (IReadDbMapping)Activator.CreateInstance(impl);
            mapping.Configure();
        }
    }
}