using System.ComponentModel.DataAnnotations;
using SocialMedia.Core.Abstractions;

namespace SocialMedia.Core.AppSettings;

public sealed class ConnectionOptions : BaseOptions
{
    public const string ConfigSectionPath = "ConnectionStrings";

    [Required]
    public string SqlConnection = "Server=localhost;Database=socialMedia;TrustServerCertificate=true;User Id=sa;password=esi@123456&";

    [Required]
    public string NoSqlConnection { get; private init; } = "mongodb://localhost:27017";

    [Required]
    public string CacheConnection { get; private init; } 
}