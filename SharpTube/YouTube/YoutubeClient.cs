namespace SharpTube.YouTube;

/// <summary>
/// Defines methods for interacting with YouTube, including operations to initialize cookies
/// and retrieve raw HTML data for playlists, videos, and channels.
/// </summary>
internal interface IYoutubeClient
{
    /// <summary>
    /// Initializes the necessary cookies required to make valid requests to YouTube.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task InitCookies();

    /// <summary>
    /// Retrieves the raw HTML content for a given playlist.
    /// </summary>
    /// <param name="playlistId">The ID of the YouTube playlist.</param>
    /// <returns>A task that returns the raw response string.</returns>
    public Task<string> GetPlaylist(string playlistId);

    /// <summary>
    /// Retrieves the raw HTML content for a given video.
    /// </summary>
    /// <param name="videoId">The ID of the YouTube video.</param>
    /// <returns>A task that returns the raw response string.</returns>
    public Task<string> GetVideo(string videoId);

    /// <summary>
    /// Retrieves the raw HTML content for a given channel.
    /// </summary>
    /// <param name="channelId">The ID or slug of the YouTube channel.</param>
    /// <returns>A task that returns the raw response string.</returns>
    public Task<string> GetChannel(string channelId);
}

/// <summary>
/// Provides methods for interacting with YouTube via HTTP requests, including initializing cookies and retrieving playlist, video, and channel data.
/// </summary>
internal class YoutubeClient : IYoutubeClient
{
    private readonly HttpClientHandler _clientHandler;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="YoutubeClient"/> class, setting up an HTTP client and cookie handler.
    /// </summary>
    internal YoutubeClient()
    {
        _clientHandler = new()
        {
            CookieContainer = new(),
            UseCookies = true
        };

        _httpClient = new(_clientHandler)
        {
            BaseAddress = new Uri("https://www.youtube.com")
        };
    }

    /// <inheritdoc />
    public async Task InitCookies()
    {
        await _httpClient.PostAsync("upgrade_visitor_cookie?eom=1", null);

        HttpClient consentClient = new(_clientHandler)
        {
            BaseAddress = new Uri("https://consent.youtube.com")
        };

        await consentClient.PostAsync("save?continue=https://www.youtube.com/&gl=HU&m=0&pc=yt&x=5&src=2&hl=en&bl=657587456&cm=2&set_eom=false&set_apyt=true&set_ytc=true", null);
    }

    /// <inheritdoc />
    public async Task<string> GetPlaylist(string playlistId)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync($"playlist?list={playlistId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    /// <inheritdoc />
    public async Task<string> GetVideo(string videoId)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync($"watch?v={videoId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    /// <inheritdoc />
    public async Task<string> GetChannel(string channelId)
    {
        if (channelId.StartsWith("UC"))
        {
            channelId = $"/channel/{channelId}";
        }

        using HttpResponseMessage response = await _httpClient.GetAsync($"{channelId}/about");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
