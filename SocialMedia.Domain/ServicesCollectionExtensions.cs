using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SocialMedia.Domain;

public static class ServicesCollectionExtensions
{
    public static void AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}