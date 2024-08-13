using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Patterns;

public static partial class PlaylistPatterns
{
    public static readonly Regex Name = NameRegex();
    public static readonly Regex VideoCount = VideoCountRegex();
    public static readonly Regex VideoId = VideoIdRegex();
    public static readonly Regex Thumbnail = ThumbnailRegex();

    [GeneratedRegex("ownerText\":\\{\"runs\":\\[{\"text\":\"(.*?)\"")]
    private static partial Regex NameRegex();

    [GeneratedRegex("stats\":\\[{\"runs\":\\[{\"text\":\"(.*?)\"")]
    private static partial Regex VideoCountRegex();

    [GeneratedRegex("videoId\":\"(.*?)\"")]
    private static partial Regex VideoIdRegex();

    [GeneratedRegex("og:image\" content=\"(.*?)\\?")]
    private static partial Regex ThumbnailRegex();
}
