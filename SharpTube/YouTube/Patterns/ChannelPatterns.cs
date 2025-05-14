using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Patterns;

/// <summary>
/// Provides compiled regular expression patterns used for extracting structured data from raw YouTube
/// channel metadata such as uploader details, thumbnails, tags, and external links.
/// </summary>
/// <remarks>
/// This static class leverages <see cref="GeneratedRegexAttribute"/> for performance benefits
/// and centralized pattern management. The regex patterns are tailored for parsing YouTube channel HTML/JSON content.
/// </remarks>
internal static partial class ChannelPatterns
{
    internal static readonly Regex PlaylistId = PlaylistIdRegex();
    internal static readonly Regex Uploader = UploaderRegex();
    internal static readonly Regex UploaderUrl = UploaderUrlRegex();
    internal static readonly Regex Thumbnail = ThumbnailRegex();
    internal static readonly Regex Tag = TagRegex();
    internal static readonly Regex Tags = TagsRegex();
    internal static readonly Regex ExternalLinkTitles = ExternalLinkTitlesRegex();
    internal static readonly Regex ExternalLinkUrls = ExternalLinkUrlsRegex();

    [GeneratedRegex("channelUrl\":\"(.*?)\"")]
    private static partial Regex PlaylistIdRegex();

    [GeneratedRegex("channelMetadataRenderer\":{\"title\":\"(.*?)\"")]
    private static partial Regex UploaderRegex();

    [GeneratedRegex("canonicalChannelUrl\":\"(.*?)\"")]
    private static partial Regex UploaderUrlRegex();

    [GeneratedRegex("avatar\":{\"thumbnails\":\\[{\"url\":\"(.*?)\"")]
    private static partial Regex ThumbnailRegex();

    [GeneratedRegex("[\"].+?[\"]|\\S+")]
    private static partial Regex TagRegex();

    [GeneratedRegex("<meta name=\"keywords\" content=\"(.*?)\">")]
    private static partial Regex TagsRegex();

    [GeneratedRegex("channelExternalLinkViewModel\":{\"title\":{\"content\":\"(.*?)\"}")]
    private static partial Regex ExternalLinkTitlesRegex();

    [GeneratedRegex("link\":{\"content\":\"(.*?)\"")]
    private static partial Regex ExternalLinkUrlsRegex();
}
