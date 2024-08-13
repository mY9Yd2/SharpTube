using System.Text.RegularExpressions;
using System.Web;

using SharpTube.YouTube.Patterns;

namespace SharpTube.YouTube;

/// <summary>
/// Represents a YouTube channel and provides methods to retrieve and manage its data.
/// </summary>
public partial class Channel : BaseCollector
{
    [GeneratedRegex("[\"].+?[\"]|\\S+")]
    private static partial Regex TagRegex();

    /// <summary>
    /// Gets the playlist Id associated with the channel.
    /// </summary>
    public string PlaylistId { get; init; }

    /// <summary>
    /// Gets the name of the channel uploader.
    /// </summary>
    public string Uploader { get; init; }

    /// <summary>
    /// Gets the Id of the channel uploader.
    /// </summary>
    public string UploaderId { get; init; }

    /// <summary>
    /// Gets the URL of the channel uploader.
    /// </summary>
    public Uri UploaderUrl { get; init; }

    /// <summary>
    /// Gets the URL of the channel's thumbnail image.
    /// </summary>
    public Uri Thumbnail { get; init; }

    /// <summary>
    /// Gets the Id of the channel.
    /// </summary>
    public string ChannelId { get; init; }

    /// <summary>
    /// Gets the URL of the channel.
    /// </summary>
    public Uri ChannelUrl { get; init; }

    /// <summary>
    /// Gets the list of tags associated with the channel.
    /// </summary>
    public List<string> Tags { get; init; }

    /// <summary>
    /// Gets a dictionary of external links associated with the channel.
    /// </summary>
    public Dictionary<string, Uri> ExternalLinks { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Channel"/> class with the specified channel Id and data.
    /// </summary>
    /// <param name="channelId">The Id of the channel.</param>
    /// <param name="data">The raw HTML and/or JS, JSON data containing information about the channel.</param>
    private Channel(string _, string data)
    {
        PlaylistId = ExtractPlaylistId(data);

        Uploader = Collect(data, ChannelPatterns.Uploader)
                .FirstOrDefault(string.Empty);

        UploaderUrl = new Uri(
                Collect(data, ChannelPatterns.UploaderUrl)
                .FirstOrDefault(string.Empty)
                .Replace("http://", "https://"));

        UploaderId = UploaderUrl.Segments[^1];

        Thumbnail = new Uri(Collect(data, ChannelPatterns.Thumbnail).FirstOrDefault(string.Empty));

        ChannelId = ExtractChannelId(data);

        ChannelUrl = new Uri($"https://www.youtube.com/channel/{ChannelId}");

        Tags = ExtractTags(data);

        ExternalLinks = ExtractExternalLinks(data);
    }

    private static Dictionary<string, Uri> ExtractExternalLinks(string data)
    {
        var externalLinkTitles = Collect(data, ChannelPatterns.ExternalLinkTitles);
        var externalLinkUrls = Collect(data, ChannelPatterns.ExternalLinkUrls);

        return externalLinkTitles
                .Zip(externalLinkUrls, (title, url) => new { title, url })
                .ToDictionary(link => link.title, link => new Uri($"https://{link.url}"));
    }

    private static List<string> ExtractTags(string data)
    {
        string decoded_tags = HttpUtility.HtmlDecode(
                Collect(data, VideoPatterns.Tags)
                .FirstOrDefault(string.Empty));

        Regex regex = TagRegex();
        MatchCollection matches = regex.Matches(decoded_tags);

        List<string> tags = matches
                .Cast<Match>()
                .Select(match => match.Value)
                .Select(value => value.StartsWith('"') && value.EndsWith('"')
                        ? value[1..^1]
                        : value)
                .ToList();

        return tags;
    }

    private static string ExtractPlaylistId(string data)
    {
        string playlistId = new Uri(
                Collect(data, ChannelPatterns.PlaylistId)
                .FirstOrDefault(string.Empty))
                .Segments[^1];

        return playlistId[..2] == "UC"
                ? $"UU{playlistId[2..]}"
                : playlistId;
    }

    private static string ExtractChannelId(string data)
    {
        string playlistId = new Uri(
                Collect(data, ChannelPatterns.PlaylistId)
                .FirstOrDefault(string.Empty))
                .Segments[^1];

        return playlistId[..2] == "UC"
                ? playlistId
                : $"UC{playlistId[2..]}";
    }

    /// <summary>
    /// Asynchronously retrieves and constructs a <see cref="Channel"/> object for the specified channel Id.
    /// </summary>
    /// <param name="channelId">The Id of the channel to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Channel"/> object.</returns>
    public static async Task<Channel> GetChannel(string channelId)
    {
        return new Channel(channelId, await Client.GetChannel(channelId));
    }
}
