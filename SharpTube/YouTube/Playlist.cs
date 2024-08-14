using SharpTube.YouTube.Patterns;

namespace SharpTube.YouTube;

/// <summary>
/// Represents a YouTube playlist and provides methods to retrieve and manage its data.
/// </summary>
public class Playlist : BaseCollector
{
    /// <summary>
    /// Gets the Id of the playlist.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Gets the URL of the playlist.
    /// </summary>
    public Uri Url { get; init; }

    /// <summary>
    /// Gets the name of the playlist.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the total number of videos in the playlist.
    /// Includes streams and upcoming videos.
    /// </summary>
    public int VideoCount { get; init; }

    /// <summary>
    /// Gets the URL of the playlist's thumbnail image.
    /// </summary>
    public Uri? Thumbnail { get; init; }

    /// <summary>
    /// Gets the list of video Ids in the playlist.
    /// </summary>
    public List<string> VideoIds { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Playlist"/> class with the specified playlist Id and data.
    /// </summary>
    /// <param name="playlistId">The Id of the playlist.</param>
    /// <param name="data">The raw HTML and/or JS, JSON data containing information about the playlist.</param>
    private Playlist(string playlistId, string data)
    {
        Id = playlistId;
        Url = new Uri($"https://www.youtube.com/playlist?list={playlistId}");
        Name = Collect(data, PlaylistPatterns.Name).FirstOrDefault(string.Empty);
        VideoCount = Convert.ToInt32(Collect(data, PlaylistPatterns.VideoCount).FirstOrDefault("0"));
        Thumbnail = GetUri(Collect(data, PlaylistPatterns.Thumbnail).FirstOrDefault());
        VideoIds = Collect(data, PlaylistPatterns.VideoId);
    }

    private static Uri? GetUri(string? url)
    {
        return string.IsNullOrEmpty(url)
                ? null
                : new Uri(url);
    }

    /// <summary>
    /// Asynchronously retrieves and constructs a <see cref="Playlist"/> object for the specified playlist Id.
    /// </summary>
    /// <param name="playlistId">The Id of the playlist to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Playlist"/> object.</returns>
    public static async Task<Playlist> GetPlaylist(string playlistId)
    {
        return new Playlist(playlistId, await Client.GetPlaylist(playlistId));
    }
}
