using System.Globalization;
using System.Text;

using SharpTube.YouTube;

namespace SharpTube.Tests;

public class SharpTubeTest
{
    [Fact]
    public async Task TestPlaylist()
    {
        // Arrange
        string playlistId = "UUhgPVLjqugDQpRLWvC7zzig";

        // Act
        await Client.InitCookies();
        Playlist playlist = await Playlist.GetPlaylist(playlistId);

        // Assert
        playlist.Name
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("ironmouse");
        playlist.Url
                .ToString()
                .Should()
                .BeEquivalentTo($"https://www.youtube.com/playlist?list={playlistId}");
        playlist.Thumbnail?
                .ToString()
                .Should()
                .NotBeNullOrEmpty();
        playlist.Id
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo(playlistId);
        playlist.VideoCount
                .Should()
                .BeGreaterThanOrEqualTo(0);
        playlist.VideoIds
                .Should()
                .NotBeNull();

        StringBuilder strBuilder = new();
        strBuilder.AppendLine("Playlist:");
        strBuilder.AppendLine($"\tName: {playlist.Name}");
        strBuilder.AppendLine($"\tUrl: {playlist.Url}");
        strBuilder.AppendLine($"\tThumbnail: {playlist.Thumbnail}");
        strBuilder.AppendLine($"\tVideoCount: {playlist.VideoCount}");
        strBuilder.AppendLine($"\tId: {playlist.Id}");
        strBuilder.AppendLine($"\tFirst 3 videoIds: {string.Join(", ", playlist.VideoIds.Take(3))}");
        Console.WriteLine(strBuilder);
    }

    [Fact]
    public async Task TestVideo()
    {
        // Arrange
        string videoId = "Wd0P-dailbY";

        // Act
        await Client.InitCookies();
        Video video = await Video.GetVideo(videoId);

        // Assert
        video.DisplayId
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("Wd0P-dailbY");
        video.FullTitle
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("Devil - Ironmouse & Bubi (Official Music Video)");
        video.Duration
                .Should()
                .BePositive()
                .And
                .BeCloseTo(200, 1);
        video.DurationString
                .Should()
                .BeEquivalentTo("3:20");
        video.MachineReadableDurationString
                .Should()
                .BeEquivalentTo("PT3M20S");
        video.Timestamp
                .ToUnixTimeSeconds()
                .Should()
                .BeCloseTo(1718492415, 1);
        video.ChannelId
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("UChgPVLjqugDQpRLWvC7zzig");
        video.ChannelUrl
                .ToString()
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig");
        video.Tags
                .Should()
                .NotBeNull()
                .And
                .Contain(["Ironmouse", "VTuber", "devil", "bubi", "official"]);
        video.Thumbnail
                .ToString()
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("https://i.ytimg.com/vi/Wd0P-dailbY/maxresdefault.jpg");
        video.OriginalUrl
                .ToString()
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("https://www.youtube.com/watch?v=Wd0P-dailbY");

        StringBuilder strBuilder = new();
        strBuilder.AppendLine("Video:");
        strBuilder.AppendLine($"\tDisplayId: {video.DisplayId}");
        strBuilder.AppendLine($"\tFullTitle: {video.FullTitle}");
        strBuilder.AppendLine($"\tDuration: {video.Duration}");
        string dateTimeFormated = video.Timestamp.UtcDateTime.ToString("s", CultureInfo.InvariantCulture);
        strBuilder.AppendLine($"\tDurationString: {video.DurationString}");
        strBuilder.AppendLine($"\tMachineReadableDurationString: {video.MachineReadableDurationString}");
        strBuilder.AppendLine($"\tTimestamp: {dateTimeFormated} ({video.Timestamp.ToUnixTimeSeconds()})");
        strBuilder.AppendLine($"\tChannelId: {video.ChannelId}");
        strBuilder.AppendLine($"\tChannelUrl: {video.ChannelUrl}");
        strBuilder.AppendLine($"\tFirst 5 tags: {string.Join(", ", video.Tags.Take(5))}");
        strBuilder.AppendLine($"\tThumbnail: {video.Thumbnail}");
        strBuilder.AppendLine($"\tOriginalUrl: {video.OriginalUrl}");
        Console.WriteLine(strBuilder);
    }

    [Fact]
    public async Task TestChannel()
    {
        // Arrange
        string channelId = "@IronMouseParty";
        List<string> expectedTags = [
            "ironmouse",
            "VTuber",
            "vshojo",
            "twitch",
            "stream",
            "mouse",
            "tungsten rat",
            "mousy",
            "virtual"];
        Dictionary<string, Uri> expectedExternalLinks = new()
        {
            { "Twitter", new Uri("https://twitter.com/ironmouse") },
            { "Twitch", new Uri("https://twitch.tv/ironmouse") },
            { "TikTok", new Uri("https://tiktok.com/@ironmouse") },
            { "VODS", new Uri("https://youtube.com/channel/UC733wqgq7RmafDY7qEACsBg") },
            { "Patreon", new Uri("https://patreon.com/ironmouse") }
        };

        // Act
        await Client.InitCookies();
        Channel channel = await Channel.GetChannel(channelId);

        // Assert
        channel.PlaylistId
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("UUhgPVLjqugDQpRLWvC7zzig");
        channel.Uploader
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("ironmouse");
        channel.UploaderId
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("@IronMouseParty");
        channel.UploaderUrl
                .ToString()
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("https://www.youtube.com/@IronMouseParty");
        channel.Thumbnail
                .ToString()
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("https://yt3.googleusercontent.com/ytc/AIdro_lnpcdCnJi5j9aL2TtXam65hLVm2Fb9wG6kYUyo9E-yi0E=s900-c-k-c0x00ffffff-no-rj");
        channel.ChannelId
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("UChgPVLjqugDQpRLWvC7zzig");
        channel.ChannelUrl
                .ToString()
                .Should()
                .NotBeNullOrEmpty()
                .And
                .BeEquivalentTo("https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig");
        channel.Tags
                .Should()
                .NotBeNull()
                .And
                .Contain(expectedTags);
        channel.ExternalLinks
                .Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(expectedExternalLinks);

        StringBuilder strBuilder = new();
        strBuilder.AppendLine("Channel:");
        strBuilder.AppendLine($"\tPlaylistId: {channel.PlaylistId}");
        strBuilder.AppendLine($"\tUploader: {channel.Uploader}");
        strBuilder.AppendLine($"\tUploaderId: {channel.UploaderId}");
        strBuilder.AppendLine($"\tUploaderUrl: {channel.UploaderUrl}");
        strBuilder.AppendLine($"\tThumbnail: {channel.Thumbnail}");
        strBuilder.AppendLine($"\tChannelId: {channel.ChannelId}");
        strBuilder.AppendLine($"\tChannelUrl: {channel.ChannelUrl}");
        strBuilder.AppendLine($"\tFirst 5 tags: {string.Join(", ", channel.Tags.Take(5))}");
        strBuilder.AppendLine($"\tExternalLinks:");
        foreach (var link in channel.ExternalLinks)
        {
            strBuilder.AppendLine($"\t\t{link.Key}: {link.Value}");
        }
        Console.WriteLine(strBuilder);
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
        // Arrange
        // ---

        // Act
        string result = Video.SecondsToDurationString(seconds);

        // Assert
        result.Should().BeEquivalentTo(exceptedDurationString);
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
        // Arrange
        // ---

        // Act
        string result = Video.SecondsToMachineReadableDurationString(seconds);

        // Assert
        result.Should().BeEquivalentTo(exceptedDurationString);
    }

    [Theory]
    [InlineData("1000", 1)]
    [InlineData("3000", 3)]
    [InlineData("60000", 60)]
    public void MillisecondsToSeconds_ShouldConvertCorrectly_ForVariousInputs(string milliseconds, int exceptedSeconds)
    {
        // Arrange
        // ---

        // Act
        int result = Video.MillisecondsToSeconds(milliseconds);

        // Assert
        result.Should().Be(exceptedSeconds);
    }
}
