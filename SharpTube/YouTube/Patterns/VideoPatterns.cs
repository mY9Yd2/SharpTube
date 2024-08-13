using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Patterns;

public static partial class VideoPatterns
{
    public static readonly Regex DisplayId = DisplayIdRegex();
    public static readonly Regex FullTitle = FullTitleRegex();
    public static readonly Regex Duration = DurationRegex();
    public static readonly Regex Timestamp = TimestampRegex();
    public static readonly Regex ChannelId = ChannelIdRegex();
    public static readonly Regex Tags = TagsRegex();
    public static readonly Regex Thumbnail = ThumbnailRegex();

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
