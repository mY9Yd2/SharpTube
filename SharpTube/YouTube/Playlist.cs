namespace SharpTube.YouTube;

/// <summary>
/// Represents a YouTube playlist and provides methods to retrieve and manage its data.
/// </summary>
public record Playlist
{
    /// <summary>
    /// Gets the Id of the playlist.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the URL of the playlist.
    /// </summary>
    public required Uri Url { get; init; }

    /// <summary>
    /// Gets the name of the playlist.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the total number of videos in the playlist.
    /// Includes streams and upcoming videos.
    /// </summary>
    public required int VideoCount { get; init; }

    /// <summary>
    /// Gets the URL of the playlist's thumbnail image.
    /// </summary>
    public required Uri? Thumbnail { get; init; }

    /// <summary>
    /// Gets the list of video Ids in the playlist.
    /// </summary>
    public required List<string> VideoIds { get; init; }
}
