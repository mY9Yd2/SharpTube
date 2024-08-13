using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Patterns;

public static partial class ChannelPatterns
{
    public static readonly Regex PlaylistId = PlaylistIdRegex();
    public static readonly Regex Uploader = UploaderRegex();
    public static readonly Regex UploaderUrl = UploaderUrlRegex();
    public static readonly Regex Thumbnail = ThumbnailRegex();
    public static readonly Regex Tags = TagsRegex();
    public static readonly Regex ExternalLinkTitles = ExternalLinkTitlesRegex();
    public static readonly Regex ExternalLinkUrls = ExternalLinkUrlsRegex();

    [GeneratedRegex("channelUrl\":\"(.*?)\"")]
    private static partial Regex PlaylistIdRegex();

    [GeneratedRegex("channelMetadataRenderer\":{\"title\":\"(.*?)\"")]
    private static partial Regex UploaderRegex();

    [GeneratedRegex("canonicalChannelUrl\":\"(.*?)\"")]
    private static partial Regex UploaderUrlRegex();

    [GeneratedRegex("avatar\":{\"thumbnails\":\\[{\"url\":\"(.*?)\"")]
    private static partial Regex ThumbnailRegex();

    [GeneratedRegex("<meta name=\"keywords\" content=\"(.*?)\">")]
    private static partial Regex TagsRegex();

    [GeneratedRegex("channelExternalLinkViewModel\":{\"title\":{\"content\":\"(.*?)\"}")]
    private static partial Regex ExternalLinkTitlesRegex();

    [GeneratedRegex("link\":{\"content\":\"(.*?)\"")]
    private static partial Regex ExternalLinkUrlsRegex();
}
