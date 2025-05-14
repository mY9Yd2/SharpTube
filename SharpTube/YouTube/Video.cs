namespace SharpTube.YouTube;

/// <summary>
/// Represents a YouTube video and provides methods to retrieve and manage its data.
/// </summary>
public record Video
{
    /// <summary>
    /// Gets the display Id of the video.
    /// </summary>
    public required string DisplayId { get; init; }

    /// <summary>
    /// Gets the full title of the video.
    /// </summary>
    public required string FullTitle { get; init; }

    /// <summary>
    /// Gets the duration of the video in seconds.
    /// </summary>
    public required int Duration { get; init; }

    /// <summary>
    /// Gets the timestamp when the video was published.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the channel Id that uploaded the video.
    /// </summary>
    public required string ChannelId { get; init; }

    /// <summary>
    /// Gets the URL of the channel that uploaded the video.
    /// </summary>
    public required Uri ChannelUrl { get; init; }

    /// <summary>
    /// Gets the list of tags associated with the video.
    /// </summary>
    public required List<string> Tags { get; init; }

    /// <summary>
    /// Gets the URL of the video's thumbnail image.
    /// </summary>
    public required Uri Thumbnail { get; init; }

    /// <summary>
    /// Gets the original URL of the video.
    /// </summary>
    public required Uri OriginalUrl { get; init; }

    /// <summary>
    /// Gets the duration of the video as a formatted string.
    /// </summary>
    public required string DurationString { get; init; }

    /// <summary>
    /// Gets the duration of the video in a machine-readable format.
    /// </summary>
    public required string MachineReadableDurationString { get; init; }
}
