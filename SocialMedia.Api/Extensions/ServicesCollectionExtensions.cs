using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.AppSettings;
using SocialMedia.Core.Extensions;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Data.Context;

namespace SocialMedia.Api.Extensions;

public static class ServicesCollectionExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SocialMedia ",
                    Description = "ASP.NET Core C# CQRS Event Sourcing, REST API, DDD, Princípios SOLID e Clean Architecture",
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Jean Gatto",
                    //    Email = "jean_gatto@hotmail.com",
                    //    Url = new Uri("https://www.linkedin.com/in/jeangatto/")
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "MIT License",
                    //    Url = new Uri("https://github.com/jeangatto/ASP.NET-Core-API-CQRS-EVENT-DDD-SOLID/blob/main/LICENSE")
                    //}
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, true);
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }

    public static void AddSocialMediaContext(this IServiceCollection services)
    {
        services.AddDbContext<WriteDbContext>((serviceProvider, options) =>
        {
            var logger = serviceProvider.GetRequiredService<ILogger<WriteDbContext>>();
            var connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionOptions>>().Value;

            options.UseSqlServer(connectionOptions.SqlConnection, sqlOptions =>
            {
                //sqlOptions.MigrationsAssembly("SocialMedia.Api");

                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);

                options.LogTo(
                    filter: (eventId, _) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
                    logger: eventData =>
                    {
                        if (eventData is not ExecutionStrategyEventData retryEventData) return;

                        var exceptions = retryEventData.ExceptionsEncountered;

                        logger.LogWarning(
                            "----- DbContext: Retry #{Count} with delay {Delay} due to error: {Message}",
                            exceptions.Count,
                            retryEventData.Delay,
                            exceptions[^1].Message);
                    });
            });

            // Quando o ambiente for o de "desenvolvimento" será logado informações detalhadas.
            var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
            if (environment.IsDevelopment())
                options.EnableDetailedErrors().EnableSensitiveDataLogging();
        });
    }

    //public static void AddCache(this IServiceCollection services, IConfiguration configuration)
    //{
    //    var connectionOptions = configuration.GetOptions<ConnectionOptions>(ConnectionOptions.ConfigSectionPath);
    //    if (connectionOptions.CacheConnection.Equals("InMemory", StringComparison.InvariantCultureIgnoreCase))
    //    {
    //        services.AddMemoryCache();
    //        services.AddMemoryCacheService();
    //    }
    //    else
    //    {
    //        services.AddDistributedRedisCache(options =>
    //        {
    //            options.InstanceName = "master";
    //            options.Configuration = connectionOptions.CacheConnection;
    //        });

    //        services.AddDistributedCacheService();
    //    }
    //}
}