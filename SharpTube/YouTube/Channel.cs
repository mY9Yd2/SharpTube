namespace SharpTube.YouTube;

/// <summary>
/// Represents a YouTube channel and provides methods to retrieve and manage its data.
/// </summary>
public record Channel
{
    /// <summary>
    /// Gets the playlist Id associated with the channel.
    /// </summary>
    public required string PlaylistId { get; init; }

    /// <summary>
    /// Gets the name of the channel uploader.
    /// </summary>
    public required string Uploader { get; init; }

    /// <summary>
    /// Gets the Id of the channel uploader.
    /// </summary>
    public required string UploaderId { get; init; }

    /// <summary>
    /// Gets the URL of the channel uploader.
    /// </summary>
    public required Uri UploaderUrl { get; init; }

    /// <summary>
    /// Gets the URL of the channel's thumbnail image.
    /// </summary>
    public required Uri Thumbnail { get; init; }

    /// <summary>
    /// Gets the Id of the channel.
    /// </summary>
    public required string ChannelId { get; init; }

    /// <summary>
    /// Gets the URL of the channel.
    /// </summary>
    public required Uri ChannelUrl { get; init; }

    /// <summary>
    /// Gets the list of tags associated with the channel.
    /// </summary>
    public required List<string> Tags { get; init; }

    /// <summary>
    /// Gets a dictionary of external links associated with the channel.
    /// </summary>
    public required Dictionary<string, Uri> ExternalLinks { get; init; }
}
