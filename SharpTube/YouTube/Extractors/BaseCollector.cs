using System.Text.RegularExpressions;

namespace SharpTube.YouTube.Extractors;

/// <summary>
/// Provides a base class for collecting data using regular expressions.
/// </summary>
internal abstract class BaseCollector
{
    /// <summary>
    /// Extracts and returns a distinct list of values captured by the first group in each match of the specified regex pattern against the input data.
    /// </summary>
    /// <param name="data">The input string to search.</param>
    /// <param name="regex">The regular expression with at least one capture group.</param>
    /// <returns>A list of distinct values captured by group 1 in the matches.</returns>
    private protected static List<string> Collect(string data, Regex regex)
    {
        return [.. regex.Matches(data)
            .Cast<Match>()
            .Select(m => m.Groups[1].Value)
            .Distinct()];
    }
}
