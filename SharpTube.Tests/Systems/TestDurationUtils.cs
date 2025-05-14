namespace SharpTube.Tests.Systems;

public class TestDurationUtils
{
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(59, "59")]
    [InlineData(60, "1:00")]
    [InlineData(61, "1:01")]
    [InlineData(3599, "59:59")]
    [InlineData(3600, "1:00:00")]
    [InlineData(86400, "1:00:00:00")]
    public void SecondsToDurationString_ShouldReturnExpectedFormat_ForVariousInputs(int seconds, string exceptedDurationString)
    {
        // Act
        string result = DurationUtils.SecondsToDurationString(seconds);

        // Assert
        Assert.Equal(exceptedDurationString, result);
    }

    [Theory]
    [InlineData(0, "PT0S")]
    [InlineData(1, "PT1S")]
    [InlineData(59, "PT59S")]
    [InlineData(60, "PT1M")]
    [InlineData(61, "PT1M1S")]
    [InlineData(3599, "PT59M59S")]
    [InlineData(3600, "PT1H")]
    [InlineData(86400, "P1D")]
    public void SecondsToMachineReadableDurationString_ShouldReturnExpectedFormat_ForVariousInputs(int seconds, string exceptedDurationString)
    {
        // Act
        string result = DurationUtils.SecondsToMachineReadableDurationString(seconds);

        // Assert
        Assert.Equal(exceptedDurationString, result);
    }

    [Theory]
    [InlineData("1000", 1)]
    [InlineData("3000", 3)]
    [InlineData("60000", 60)]
    public void MillisecondsToSeconds_ShouldConvertCorrectly_ForVariousInputs(string milliseconds, int exceptedSeconds)
    {
        // Act
        int result = DurationUtils.MillisecondsToSeconds(milliseconds);

        // Assert
        Assert.Equal(exceptedSeconds, result);
    }
}
