using SharpTube.YouTube.Patterns;

namespace SharpTube.YouTube.Extractors;

/// <summary>
/// Defines a contract for extracting a <see cref="Playlist"/> object from raw YouTube playlist data.
/// </summary>
internal interface IPlaylistExtractor
{
    /// <summary>
    /// Extracts a <see cref="Playlist"/> object from the provided raw YouTube video data.
    /// </summary>
    /// <param name="playlistId">The unique ID of the YouTube playlist.</param>
    /// <param name="data">The raw HTML or JSON response containing playlist metadata.</param>
    /// <returns>A populated <see cref="Playlist"/> instance.</returns>
    public Playlist Extract(string playlistId, string data);
}

internal class PlaylistExtractor : BaseCollector, IPlaylistExtractor
{
    /// <inheritdoc />
    public Playlist Extract(string playlistId, string data)
    {
        return new Playlist
        {
            Id = playlistId,
            Url = ConstructUrl(playlistId),
            Name = ExtractName(data),
            VideoCount = ExtractVideoCount(data),
            Thumbnail = ExtractThumbnail(data),
            VideoIds = ExtractVideoIds(data),
        };
    }

    /// <summary>
    /// Extracts a list of video IDs from the playlist data.
    /// </summary>
    /// <param name="data">The raw playlist data.</param>
    /// <returns>A list of video IDs.</returns>
    private static List<string> ExtractVideoIds(string data)
    {
        return Collect(data, PlaylistPatterns.VideoId);
    }

    /// <summary>
    /// Extracts the playlist thumbnail URL and converts it to a <see cref="Uri"/>.
    /// </summary>
    /// <param name="data">The raw playlist data.</param>
    /// <returns>A <see cref="Uri"/> representing the thumbnail, or <c>null</c> if not found.</returns>
    private static Uri? ExtractThumbnail(string data)
    {
        return GetUri(Collect(data, PlaylistPatterns.Thumbnail).FirstOrDefault());
    }

    /// <summary>
    /// Extracts the total number of videos in the playlist.
    /// </summary>
    /// <param name="data">The raw playlist data.</param>
    /// <returns>The number of videos in the playlist.</returns>
    private static int ExtractVideoCount(string data)
    {
        return Convert.ToInt32(Collect(data, PlaylistPatterns.VideoCount).FirstOrDefault("0"));
    }

    /// <summary>
    /// Extracts the name/title of the playlist.
    /// </summary>
    /// <param name="data">The raw playlist data.</param>
    /// <returns>The name of the playlist, or an empty string if not found.</returns>
    private static string ExtractName(string data)
    {
        return Collect(data, PlaylistPatterns.Name).FirstOrDefault(string.Empty);
    }

    /// <summary>
    /// Constructs the full YouTube URL for the playlist using its Id.
    /// </summary>
    /// <param name="playlistId">The unique identifier of the playlist.</param>
    /// <returns>The full <see cref="Uri"/> to the playlist.</returns>
    private static Uri ConstructUrl(string playlistId)
    {
        return new Uri($"https://www.youtube.com/playlist?list={playlistId}");
    }

    /// <summary>
    /// Safely converts a string URL to a <see cref="Uri"/>, returning <c>null</c> if the string is empty or null.
    /// </summary>
    /// <param name="url">The string representation of the URL.</param>
    /// <returns>A <see cref="Uri"/> object or <c>null</c> if the input is null or empty.</returns>
    private static Uri? GetUri(string? url)
    {
        return string.IsNullOrEmpty(url)
            ? null
            : new Uri(url);
    }
}
