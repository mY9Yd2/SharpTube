using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Patterns;

/// <summary>
/// Provides compiled regular expression patterns for extracting structured data from raw YouTube
/// video metadata, such as video ID, title, duration, timestamp, channel information, tags, and thumbnails.
/// </summary>
/// <remarks>
/// This static partial class utilizes the <see cref="GeneratedRegexAttribute"/> to compile
/// regular expressions at build time, enhancing performance. The patterns are specifically
/// tailored to parse YouTube video-related HTML or JSON responses.
/// </remarks>
internal static partial class VideoPatterns
{
    internal static readonly Regex DisplayId = DisplayIdRegex();
    internal static readonly Regex FullTitle = FullTitleRegex();
    internal static readonly Regex Duration = DurationRegex();
    internal static readonly Regex Timestamp = TimestampRegex();
    internal static readonly Regex ChannelId = ChannelIdRegex();
    internal static readonly Regex Tags = TagsRegex();
    internal static readonly Regex Thumbnail = ThumbnailRegex();

    [GeneratedRegex("videoId\":\"(.*?)\"")]
    private static partial Regex DisplayIdRegex();

    [GeneratedRegex("title\":\"(.*?)\"")]
    private static partial Regex FullTitleRegex();

    [GeneratedRegex("approxDurationMs\":\"(.*?)\"")]
    private static partial Regex DurationRegex();

    [GeneratedRegex("uploadDate\":\"(.*?)\"")]
    private static partial Regex TimestampRegex();

    [GeneratedRegex("channelIds\":\\[\"(.*?)\"")]
    private static partial Regex ChannelIdRegex();

    [GeneratedRegex("<meta name=\"keywords\" content=\"(.*?)\">")]
    private static partial Regex TagsRegex();

    [GeneratedRegex("playerMicroformatRenderer\":{\"thumbnail\":{\"thumbnails\":\\[{\"url\":\"(.*?)\"")]
    private static partial Regex ThumbnailRegex();
}
