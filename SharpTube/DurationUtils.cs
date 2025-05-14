using System.Text;

namespace SharpTube;

/// <summary>
/// Provides utility methods for working with durations, including converting milliseconds to seconds
/// and formatting durations into human-readable and machine-readable strings.
/// </summary>
internal static class DurationUtils
{

    /// <summary>
    /// Converts a string representing milliseconds to seconds.
    /// </summary>
    /// <param name="milliseconds">The string representation of the time in milliseconds.</param>
    /// <returns>The equivalent time in seconds as an integer.</returns>
    /// <exception cref="FormatException">Thrown if the <paramref name="milliseconds"/> cannot be converted to a valid integer.</exception>
    /// <exception cref="OverflowException">Thrown if the <paramref name="milliseconds"/> value is too large or too small for a <see cref="long"/>.</exception>
    internal static int MillisecondsToSeconds(string milliseconds)
    {
        return (int)(Convert.ToInt64(milliseconds) / 1000);
    }

    /// <summary>
    /// Converts a total number of seconds into a human-readable string format (e.g., "hh:mm:ss", "mm:ss", etc.).
    /// </summary>
    /// <param name="totalSeconds">The total duration in seconds.</param>
    /// <returns>A string representing the duration in a user-friendly format.</returns>
    /// <remarks>
    /// If the duration is over 24 hours, it will return a format of "dd:hh:mm:ss".
    /// Otherwise, it will return formats like "hh:mm:ss" or "mm:ss" based on the length of the duration.
    /// </remarks>
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

    /// <summary>
    /// Converts a total number of seconds into a machine-readable ISO 8601 duration string format (e.g., "PT1H2M3S").
    /// </summary>
    /// <param name="totalSeconds">The total duration in seconds.</param>
    /// <returns>A string representing the duration in a machine-readable format.</returns>
    /// <remarks>
    /// The format follows the ISO 8601 duration format: "P" for the period (days), and "T" for the time section (hours, minutes, seconds).
    /// Example: "P1DT2H3M4S" represents "1 day, 2 hours, 3 minutes, 4 seconds".
    /// </remarks>
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

            if (hours > 0) durationBuilder.Append($"{hours}H");
            if (minutes > 0) durationBuilder.Append($"{minutes}M");
            if (seconds > 0) durationBuilder.Append($"{seconds}S");
        }

        string duration = durationBuilder.ToString();

        return duration == "P"
            ? "PT0S"
            : duration;
    }
}
