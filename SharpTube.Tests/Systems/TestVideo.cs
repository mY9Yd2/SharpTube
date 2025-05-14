using SharpTube.YouTube;

namespace SharpTube.Tests.Systems;

public class TestVideo
{
    [Fact]
    public async Task GetVideo_ReturnsExpectedMetadata_ForValidVideoId()
    {
        // Arrange
        string videoId = "Wd0P-dailbY";

        var expectedDuration = 200;
        var durationPrecision = 1;

        var expectedTimestamp = 1718492415;
        var timestampPrecision = 1;

        List<string> expectedTags = ["Ironmouse", "VTuber", "devil", "bubi", "official"];

        // Act
        await Client.InitCookies();
        Video video = await Video.GetVideo(videoId);

        // Assert
        Assert.Equal("Wd0P-dailbY", video.DisplayId);
        Assert.Equal("Devil - Ironmouse & Bubi (Official Music Video)", video.FullTitle);
        Assert.Equal("3:20", video.DurationString);
        Assert.Equal("PT3M20S", video.MachineReadableDurationString);
        Assert.Equal("UChgPVLjqugDQpRLWvC7zzig", video.ChannelId);
        Assert.Equal("https://i.ytimg.com/vi/Wd0P-dailbY/maxresdefault.jpg", video.Thumbnail.ToString());
        Assert.Equal("https://www.youtube.com/watch?v=Wd0P-dailbY", video.OriginalUrl.ToString());
        Assert.Equal("https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig", video.ChannelUrl.ToString());

        Assert.True(video.Duration > (expectedDuration - durationPrecision)
            && video.Duration < (expectedDuration + durationPrecision));

        Assert.True(video.Timestamp.ToUnixTimeSeconds() > (expectedTimestamp - timestampPrecision)
            && video.Timestamp.ToUnixTimeSeconds() < (expectedTimestamp + timestampPrecision));

        foreach (var tag in expectedTags)
        {
            Assert.Contains(tag, video.Tags);
        }
    }

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
        string result = Video.SecondsToDurationString(seconds);

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
        string result = Video.SecondsToMachineReadableDurationString(seconds);

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
        int result = Video.MillisecondsToSeconds(milliseconds);

        // Assert
        Assert.Equal(exceptedSeconds, result);
    }
}
