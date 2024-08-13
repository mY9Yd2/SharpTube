using System.Text.RegularExpressions;

namespace SharpTube.YouTube;

/// <summary>
/// Provides a base class for collecting data using regular expressions.
/// </summary>
public abstract class BaseCollector
{
    internal static List<string> Collect(string data, Regex regex)
    {
        return regex.Matches(data)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToList();
    }
}
