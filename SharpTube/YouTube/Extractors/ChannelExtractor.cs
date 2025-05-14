using System.Text.RegularExpressions;
using System.Web;

using SharpTube.YouTube.Patterns;

namespace SharpTube.YouTube.Extractors;

/// <summary>
/// Defines a contract for extracting structured <see cref="Channel"/> information
/// from raw YouTube response data.
/// </summary>
internal interface IChannelExtractor
{
    /// <summary>
    /// Extracts a <see cref="Channel"/> object from the provided raw YouTube video data.
    /// </summary>
    /// <param name="data">The raw HTML or JSON string containing channel data.</param>
    /// <returns>A <see cref="Channel"/> object populated with extracted information.</returns>
    public Channel Extract(string data);
}

/// <summary>
/// Implements <see cref="IChannelExtractor"/> to extract structured channel information
/// from YouTube response data using regular expressions.
/// </summary>
internal class ChannelExtractor : BaseCollector, IChannelExtractor
{
    /// <inheritdoc />
    public Channel Extract(string data)
    {
        return new Channel
        {
            PlaylistId = ExtractPlaylistId(data),
            Uploader = ExtractUploader(data),
            UploaderUrl = ExtractUploaderUrl(data),
            UploaderId = ExtractUploaderId(data),
            Thumbnail = ExtractThumbnail(data),
            ChannelId = ExtractChannelId(data),
            ChannelUrl = ConstructChannelUrl(data),
            Tags = ExtractTags(data),
            ExternalLinks = ExtractExternalLinks(data),
        };
    }

    /// <summary>
    /// Extracts the channel's thumbnail URL from the provided data.
    /// </summary>
    private static Uri ExtractThumbnail(string data)
    {
        return new Uri(Collect(data, ChannelPatterns.Thumbnail).FirstOrDefault(string.Empty));
    }

    /// <summary>
    /// Extracts the uploader's display name from the provided data.
    /// </summary>
    private static string ExtractUploader(string data)
    {
        return Collect(data, ChannelPatterns.Uploader).FirstOrDefault(string.Empty);
    }

    /// <summary>
    /// Constructs the full URL to the YouTube channel.
    /// </summary>
    private static Uri ConstructChannelUrl(string data)
    {
        return new Uri($"https://www.youtube.com/channel/{ExtractChannelId(data)}");
    }

    /// <summary>
    /// Extracts the uploader's unique ID segment from their URL.
    /// </summary>
    private static string ExtractUploaderId(string data)
    {
        return ExtractUploaderUrl(data).Segments[^1];
    }

    /// <summary>
    /// Extracts the uploader's URL from the provided data.
    /// </summary>
    private static Uri ExtractUploaderUrl(string data)
    {
        return new Uri(Collect(data, ChannelPatterns.UploaderUrl)
            .FirstOrDefault(string.Empty)
            .Replace("http://", "https://"));
    }

    /// <summary>
    /// Extracts external links such as social profiles from the channel's description.
    /// </summary>
    /// <returns>A dictionary where keys are link titles and values are full URLs.</returns>
    private static Dictionary<string, Uri> ExtractExternalLinks(string data)
    {
        var externalLinkTitles = Collect(data, ChannelPatterns.ExternalLinkTitles);
        var externalLinkUrls = Collect(data, ChannelPatterns.ExternalLinkUrls);

        return externalLinkTitles
            .Zip(externalLinkUrls, (title, url) => new { title, url })
            .ToDictionary(link => link.title, link => new Uri($"https://{link.url}"));
    }

    /// <summary>
    /// Extracts the list of tags or keywords associated with the channel.
    /// </summary>
    /// <returns>A list of decoded tag strings.</returns>
    private static List<string> ExtractTags(string data)
    {
        string decoded_tags = HttpUtility.HtmlDecode(
            Collect(data, VideoPatterns.Tags).FirstOrDefault(string.Empty));

        MatchCollection matches = ChannelPatterns.Tag.Matches(decoded_tags);

        List<string> tags = [.. matches
            .Cast<Match>()
            .Select(match => match.Value)
            .Select(value => value.StartsWith('"') && value.EndsWith('"')
                ? value[1..^1]
                : value)];

        return tags;
    }

    /// <summary>
    /// Extracts the channel's associated uploads playlist ID (typically prefixed with "UU").
    /// </summary>
    private static string ExtractPlaylistId(string data)
    {
        string playlistId = new Uri(Collect(data, ChannelPatterns.PlaylistId)
            .FirstOrDefault(string.Empty)).Segments[^1];

        return playlistId[..2] == "UC"
            ? $"UU{playlistId[2..]}"
            : playlistId;
    }

    /// <summary>
    /// Extracts the canonical YouTube channel ID (typically prefixed with "UC").
    /// </summary>
    private static string ExtractChannelId(string data)
    {
        string playlistId = new Uri(Collect(data, ChannelPatterns.PlaylistId)
            .FirstOrDefault(string.Empty)).Segments[^1];

        return playlistId[..2] == "UC"
            ? playlistId
            : $"UC{playlistId[2..]}";
    }
}
