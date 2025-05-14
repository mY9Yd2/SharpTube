using System.Globalization;
using System.Text.RegularExpressions;

using SharpTube.YouTube.Patterns;

namespace SharpTube.YouTube.Extractors;

/// <summary>
/// Defines a contract for extracting structured <see cref="Video"/> information
/// from raw YouTube response data.
/// </summary>
internal interface IVideoExtractor
{
    /// <summary>
    /// Extracts a <see cref="Video"/> object from the provided raw YouTube video data.
    /// </summary>
    /// <param name="videoId">The unique video ID used to identify the video on YouTube.</param>
    /// <param name="data">The raw HTML or JSON response containing video metadata.</param>
    /// <returns>A <see cref="Video"/> object populated with extracted information.</returns>
    public Video Extract(string videoId, string data);
}

/// <summary>
/// Extracts video-related information from raw YouTube response data using regex patterns.
/// Implements <see cref="IVideoExtractor"/>.
/// </summary>
internal class VideoExtractor : BaseCollector, IVideoExtractor
{
    /// <inheritdoc />
    public Video Extract(string videoId, string data)
    {
        return new Video
        {
            DisplayId = ExtractDisplayId(data),
            FullTitle = ExtractFullTitle(data),
            Duration = ExtractDuration(data),
            Timestamp = ExtractTimestamp(data),
            ChannelId = ExtractChannelId(data),
            ChannelUrl = ConstructChannelUrl(data),
            Tags = ExtractTags(data),
            Thumbnail = ExtractThumbnail(data),
            OriginalUrl = ConstructOriginalUrl(data),
            DurationString = GetReadableDuration(data),
            MachineReadableDurationString = GetMachineReadableDuration(data),
        };
    }

    /// <summary>
    /// Converts the extracted duration to a machine-readable ISO 8601 duration string.
    /// </summary>
    private static string GetMachineReadableDuration(string data)
    {
        return DurationUtils.SecondsToMachineReadableDurationString(ExtractDuration(data));
    }

    /// <summary>
    /// Converts the extracted duration to a human-readable format (e.g., "5 minutes").
    /// </summary>
    private static string GetReadableDuration(string data)
    {
        return DurationUtils.SecondsToDurationString(ExtractDuration(data));
    }

    /// <summary>
    /// Constructs the full YouTube video URL using the display ID.
    /// </summary>
    private static Uri ConstructOriginalUrl(string data)
    {
        return new Uri($"https://www.youtube.com/watch?v={ExtractDisplayId(data)}");
    }

    /// <summary>
    /// Extracts the video’s thumbnail URL from the raw data.
    /// </summary>
    private static Uri ExtractThumbnail(string data)
    {
        return new Uri(Collect(data, VideoPatterns.Thumbnail).FirstOrDefault(string.Empty));
    }

    /// <summary>
    /// Extracts a list of tags associated with the video.
    /// </summary>
    private static List<string> ExtractTags(string data)
    {
        return [.. Collect(data, VideoPatterns.Tags).FirstOrDefault(string.Empty).Split(", ")];
    }

    /// <summary>
    /// Constructs the full URL to the channel that uploaded the video.
    /// </summary>
    private static Uri ConstructChannelUrl(string data)
    {
        return new Uri($"https://www.youtube.com/channel/{ExtractChannelId(data)}");
    }

    /// <summary>
    /// Extracts the uploader's YouTube channel ID from the raw data.
    /// </summary>
    private static string ExtractChannelId(string data)
    {
        return Collect(data, VideoPatterns.ChannelId).FirstOrDefault(string.Empty);
    }

    /// <summary>
    /// Extracts the upload timestamp and converts it to a <see cref="DateTimeOffset"/>.
    /// </summary>
    private static DateTimeOffset ExtractTimestamp(string data)
    {
        string timestamp = Collect(data, VideoPatterns.Timestamp).FirstOrDefault(string.Empty);
        return DateTimeOffset.Parse(timestamp, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Extracts the duration of the video (in seconds) from the raw data.
    /// </summary>
    private static int ExtractDuration(string data)
    {
        string duration = Collect(data, VideoPatterns.Duration).FirstOrDefault("0");
        return DurationUtils.MillisecondsToSeconds(duration);
    }


    /// <summary>
    /// Extracts and unescapes the full title of the video.
    /// </summary>
    private static string ExtractFullTitle(string data)
    {
        return Regex.Unescape(Collect(data, VideoPatterns.FullTitle).FirstOrDefault(string.Empty));
    }

    /// <summary>
    /// Extracts the YouTube display ID used in the video URL.
    /// </summary>
    private static string ExtractDisplayId(string data)
    {
        return Collect(data, VideoPatterns.DisplayId).FirstOrDefault(string.Empty);
    }
}
