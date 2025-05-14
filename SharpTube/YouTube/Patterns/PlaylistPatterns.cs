using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Patterns;

/// <summary>
/// Provides compiled regular expression patterns for extracting structured data from raw YouTube
/// playlist metadata, such as playlist name, video count, video IDs, and thumbnail URLs.
/// </summary>
/// <remarks>
/// This static partial class uses the <see cref="GeneratedRegexAttribute"/> to compile regex patterns
/// at build time, offering better performance and maintainability. These patterns are specifically
/// designed for parsing YouTube playlist-related HTML or JSON data.
/// </remarks>
internal static partial class PlaylistPatterns
{
    internal static readonly Regex Name = NameRegex();
    internal static readonly Regex VideoCount = VideoCountRegex();
    internal static readonly Regex VideoId = VideoIdRegex();
    internal static readonly Regex Thumbnail = ThumbnailRegex();

    [GeneratedRegex("ownerText\":\\{\"runs\":\\[{\"text\":\"(.*?)\"")]
    private static partial Regex NameRegex();

    [GeneratedRegex("stats\":\\[{\"runs\":\\[{\"text\":\"(.*?)\"")]
    private static partial Regex VideoCountRegex();

    [GeneratedRegex("videoId\":\"(.*?)\"")]
    private static partial Regex VideoIdRegex();

    [GeneratedRegex("og:image\" content=\"(.*?)\\?")]
    private static partial Regex ThumbnailRegex();
}
