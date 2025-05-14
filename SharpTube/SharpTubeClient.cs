using SharpTube.YouTube;
using SharpTube.YouTube.Extractors;

namespace SharpTube;

/// <summary>
/// Defines the operations that the <see cref="SharpTubeClient"/> should implement to interact with YouTube data.
/// </summary>
public interface ISharpTubeClient
{
    /// <summary>
    /// Initializes the client by setting up necessary configurations, such as cookies,
    /// required for subsequent API requests.
    /// </summary>
    /// <remarks>
    /// This method must be called before any other methods like <see cref="GetChannel"/>, <see cref="GetPlaylist"/>, or <see cref="GetVideo"/>
    /// to ensure the client is properly initialized and ready to make requests.
    /// </remarks>
    /// <returns>A task representing the asynchronous operation of initializing the client.</returns>
    public Task Init();

    /// <summary>
    /// Asynchronously retrieves and constructs a <see cref="Channel"/> object for the specified channel Id.
    /// </summary>
    /// <param name="channelId">The Id of the channel to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Channel"/> object.</returns>
    public Task<Channel> GetChannel(string channelId);

    /// <summary>
    /// Asynchronously retrieves and constructs a <see cref="Playlist"/> object for the specified playlist Id.
    /// </summary>
    /// <param name="playlistId">The Id of the playlist to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Playlist"/> object.</returns>
    public Task<Playlist> GetPlaylist(string playlistId);

    /// <summary>
    /// Asynchronously retrieves and constructs a <see cref="Video"/> object for the specified video Id.
    /// </summary>
    /// <param name="videoId">The Id of the video to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Video"/> object.</returns>
    public Task<Video> GetVideo(string videoId);
}

/// <summary>
/// The client responsible for interacting with the YouTube API, retrieving data for channels, playlists, and videos.
/// Implements the <see cref="ISharpTubeClient"/> interface to provide methods for fetching YouTube data.
/// </summary>
public class SharpTubeClient : ISharpTubeClient
{
    private readonly IYoutubeClient _youtubeClient;
    private readonly IChannelExtractor _channelExtractor;
    private readonly IPlaylistExtractor _playlistExtractor;
    private readonly IVideoExtractor _videoExtractor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SharpTubeClient"/> class using default implementations.
    /// </summary>
    /// <remarks>
    /// Initializes the client with a <see cref="YoutubeClient"/> and default extractors for channel, playlist, and video data.
    /// </remarks>
    public SharpTubeClient() : this(
        new YoutubeClient(),
        new ChannelExtractor(),
        new PlaylistExtractor(),
        new VideoExtractor())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SharpTubeClient"/> class with custom service implementations.
    /// </summary>
    /// <param name="youtubeClient">An implementation of <see cref="IYoutubeClient"/> for handling YouTube HTTP interactions.</param>
    /// <param name="channelExtractor">An implementation of <see cref="IChannelExtractor"/> for extracting channel data.</param>
    /// <param name="playlistExtractor">An implementation of <see cref="IPlaylistExtractor"/> for extracting playlist data.</param>
    /// <param name="videoExtractor">An implementation of <see cref="IVideoExtractor"/> for extracting video data.</param>
    internal SharpTubeClient(
        IYoutubeClient youtubeClient,
        IChannelExtractor channelExtractor,
        IPlaylistExtractor playlistExtractor,
        IVideoExtractor videoExtractor)
    {
        _youtubeClient = youtubeClient;
        _channelExtractor = channelExtractor;
        _playlistExtractor = playlistExtractor;
        _videoExtractor = videoExtractor;
    }

    /// <inheritdoc />
    public async Task Init()
    {
        await _youtubeClient.InitCookies();
    }

    /// <inheritdoc />
    public async Task<Channel> GetChannel(string channelId)
    {
        string data = await _youtubeClient.GetChannel(channelId);
        return _channelExtractor.Extract(data);
    }

    /// <inheritdoc />
    public async Task<Playlist> GetPlaylist(string playlistId)
    {
        string data = await _youtubeClient.GetPlaylist(playlistId);
        return _playlistExtractor.Extract(playlistId, data);
    }

    /// <inheritdoc />
    public async Task<Video> GetVideo(string videoId)
    {
        string data = await _youtubeClient.GetVideo(videoId);
        return _videoExtractor.Extract(videoId, data);
    }
}
