using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

#nullable disable
namespace SocialMedia.Core.Extensions;

public static class JsonExtensions
{
    private static readonly CamelCaseNamingStrategy NamingStrategy = new();
    private static readonly StringEnumConverter EnumConverter = new(NamingStrategy);
    private static readonly PrivateSetterContractResolver ContractResolver = new() { NamingStrategy = NamingStrategy };
    private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings().Configure();

    public static T FromJson<T>(this string value)
        => value != null ? JsonConvert.DeserializeObject<T>(value, JsonSettings) : default;

    public static string ToJson<T>(this T value)
        => value != null ? JsonConvert.SerializeObject(value, JsonSettings) : default;

    public static JsonSerializerSettings Configure(this JsonSerializerSettings settings)
    {
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.Formatting = Formatting.None;
        settings.ContractResolver = ContractResolver;
        settings.Converters.Add(EnumConverter);
        return settings;
    }
}