using SharpTube.YouTube;

namespace SharpTube.Tests.Systems;

public class TestChannel
{
    [Fact]
    public async Task GetChannel_ReturnsExpectedChannelData_ForValidHandle()
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
            { "Clips", new Uri("https://youtube.com/@MouseClipsOfficial") },
            { "VODS", new Uri("https://youtube.com/channel/UC733wqgq7RmafDY7qEACsBg") },
            { "Patreon", new Uri("https://patreon.com/ironmouse") },
        };

        // Act
        await Client.InitCookies();
        Channel channel = await Channel.GetChannel(channelId);

        // Assert
        Assert.Equal("UUhgPVLjqugDQpRLWvC7zzig", channel.PlaylistId);
        Assert.Equal("ironmouse", channel.Uploader);
        Assert.Equal("@IronMouseParty", channel.UploaderId);
        Assert.Equal("https://www.youtube.com/@IronMouseParty", channel.UploaderUrl.ToString());
        Assert.Equal("https://yt3.googleusercontent.com/ytc/AIdro_kRmBUGJyEGZ46bHqlcqAo-yEntsTm2c0vmDzQb3jck978=s900-c-k-c0x00ffffff-no-rj", channel.Thumbnail.ToString());
        Assert.Equal("UChgPVLjqugDQpRLWvC7zzig", channel.ChannelId);
        Assert.Equal("https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig", channel.ChannelUrl.ToString());

        expectedTags.ForEach(expectedTag => Assert.Contains(expectedTag, channel.Tags));

        foreach (var expectedExternalLink in expectedExternalLinks)
        {
            Assert.Contains(expectedExternalLink, channel.ExternalLinks);
        }
    }
}
