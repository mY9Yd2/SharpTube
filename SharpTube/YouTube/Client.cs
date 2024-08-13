using System.Net;

namespace SharpTube.YouTube;

/// <summary>
/// Provides methods for interacting with YouTube via HTTP requests, including initializing cookies and retrieving playlist, video, and channel data.
/// </summary>
public static class Client
{
    private static readonly CookieContainer CookieContainer = new();
    private static readonly HttpClientHandler ClientHandler = new()
    {
        CookieContainer = CookieContainer,
        UseCookies = true
    };
    private static readonly HttpClient HttpClient = new(ClientHandler)
    {
        BaseAddress = new Uri("https://www.youtube.com")
    };

    /// <summary>
    /// Initializes the necessary cookies for making requests to YouTube.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task InitCookies()
    {
        await HttpClient.PostAsync("upgrade_visitor_cookie?eom=1", null);

        HttpClient consentClient = new(ClientHandler)
        {
            BaseAddress = new Uri("https://consent.youtube.com")
        };

        await consentClient.PostAsync("save?continue=https://www.youtube.com/&gl=HU&m=0&pc=yt&x=5&src=2&hl=en&bl=657587456&cm=2&set_eom=false&set_apyt=true&set_ytc=true", null);
    }

    internal static async Task<string> GetPlaylist(string playlistId)
    {
        using HttpResponseMessage response = await HttpClient.GetAsync($"playlist?list={playlistId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    internal static async Task<string> GetVideo(string videoId)
    {
        using HttpResponseMessage response = await HttpClient.GetAsync($"watch?v={videoId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    internal static async Task<string> GetChannel(string channelId)
    {
        if (channelId.StartsWith("UC"))
        {
            channelId = $"/channel/{channelId}";
        }

        using HttpResponseMessage response = await HttpClient.GetAsync($"{channelId}/about");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
