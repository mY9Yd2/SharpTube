using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using SharpTube.YouTube.Patterns;

namespace SharpTube.YouTube;

/// <summary>
/// Represents a YouTube video and provides methods to retrieve and manage its data.
/// </summary>
public class Video : BaseCollector
{
    /// <summary>
    /// Gets the display Id of the video.
    /// </summary>
    public string DisplayId { get; init; }

    /// <summary>
    /// Gets the full title of the video.
    /// </summary>
    public string FullTitle { get; init; }

    /// <summary>
    /// Gets the duration of the video in seconds.
    /// </summary>
    public int Duration { get; init; }

    /// <summary>
    /// Gets the timestamp when the video was published.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the channel Id that uploaded the video.
    /// </summary>
    public string ChannelId { get; init; }

    /// <summary>
    /// Gets the URL of the channel that uploaded the video.
    /// </summary>
    public Uri ChannelUrl { get; init; }

    /// <summary>
    /// Gets the list of tags associated with the video.
    /// </summary>
    public List<string> Tags { get; init; }

    /// <summary>
    /// Gets the URL of the video's thumbnail image.
    /// </summary>
    public Uri Thumbnail { get; init; }

    /// <summary>
    /// Gets the original URL of the video.
    /// </summary>
    public Uri OriginalUrl { get; init; }

    /// <summary>
    /// Gets the duration of the video as a formatted string.
    /// </summary>
    public string DurationString { get; init; }

    /// <summary>
    /// Gets the duration of the video in a machine-readable format.
    /// </summary>
    public string MachineReadableDurationString { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Video"/> class with the specified video Id and data.
    /// </summary>
    /// <param name="videoId">The Id of the video.</param>
    /// <param name="data">The raw HTML and/or JS, JSON data containing information about the video.</param>
    private Video(string _, string data)
    {
        DisplayId = Collect(data, VideoPatterns.DisplayId)
                .FirstOrDefault(string.Empty);

        FullTitle = Regex.Unescape(
                Collect(data, VideoPatterns.FullTitle)
                .FirstOrDefault(string.Empty));

        Duration = MillisecondsToSeconds(
            Collect(data, VideoPatterns.Duration)
            .FirstOrDefault("0"));

        Timestamp = DateTimeOffset.Parse(
                Collect(data, VideoPatterns.Timestamp)
                .FirstOrDefault(string.Empty), CultureInfo.InvariantCulture);

        ChannelId = Collect(data, VideoPatterns.ChannelId)
                .FirstOrDefault(string.Empty);

        ChannelUrl = new Uri($"https://www.youtube.com/channel/{ChannelId}");

        Tags = [.. Collect(data, VideoPatterns.Tags)
                .FirstOrDefault(string.Empty)
                .Split(", ")];

        Thumbnail = new Uri(
                Collect(data, VideoPatterns.Thumbnail)
                .FirstOrDefault(string.Empty));

        OriginalUrl = new Uri($"https://www.youtube.com/watch?v={DisplayId}");

        DurationString = SecondsToDurationString(Duration);

        MachineReadableDurationString = SecondsToMachineReadableDurationString(Duration);
    }

    /// <summary>
    /// Asynchronously retrieves and constructs a <see cref="Video"/> object for the specified video Id.
    /// </summary>
    /// <param name="videoId">The Id of the video to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Video"/> object.</returns>
    public static async Task<Video> GetVideo(string videoId)
    {
        return new Video(videoId, await Client.GetVideo(videoId));
    }

    internal static int MillisecondsToSeconds(string milliseconds)
    {
        return (int)(Convert.ToInt64(milliseconds) / 1000);
    }

    internal static string SecondsToDurationString(int totalSeconds)
    {
        int days = totalSeconds / 86400;
        int hours = totalSeconds % 86400 / 3600;
        int minutes = totalSeconds % 3600 / 60;
        int seconds = totalSeconds % 60;

        if (days > 0)
        {
            return $"{days}:{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        else if (hours > 0)
        {
            return $"{hours}:{minutes:D2}:{seconds:D2}";
        }
        else if (minutes > 0)
        {
            return $"{minutes}:{seconds:D2}";
        }
        else
        {
            return $"{seconds}";
        }
    }

    internal static string SecondsToMachineReadableDurationString(int totalSeconds)
    {
        int days = totalSeconds / 86400;
        int hours = totalSeconds % 86400 / 3600;
        int minutes = totalSeconds % 3600 / 60;
        int seconds = totalSeconds % 60;

        StringBuilder durationBuilder = new("P");

        if (days > 0)
        {
            durationBuilder.Append($"{days}D");
        }

        if (hours > 0 || minutes > 0 || seconds > 0)
        {
            durationBuilder.Append('T');

            if (hours > 0)
            {
                durationBuilder.Append($"{hours}H");
            }

            if (minutes > 0)
            {
                durationBuilder.Append($"{minutes}M");
            }

            if (seconds > 0)
            {
                durationBuilder.Append($"{seconds}S");
            }
        }

        string duration = durationBuilder.ToString();

        return duration == "P" ? "PT0S" : duration;
    }
}
