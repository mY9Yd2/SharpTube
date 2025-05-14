using SharpTube.YouTube;
using SharpTube.YouTube.Extractors;

namespace SharpTube.Tests.Systems;

public class TestSharpTubeClient
{
    private readonly IFixture _fixture;
    private readonly IYoutubeClient _youtubeClient;
    private readonly ISharpTubeClient _client;
    private readonly IChannelExtractor _channelExtractor;
    private readonly IPlaylistExtractor _playlistExtractor;
    private readonly IVideoExtractor _videoExtractor;

    public TestSharpTubeClient()
    {
        _fixture = new Fixture();

        _youtubeClient = Substitute.For<IYoutubeClient>();
        _channelExtractor = Substitute.For<IChannelExtractor>();
        _playlistExtractor = Substitute.For<IPlaylistExtractor>();
        _videoExtractor = Substitute.For<IVideoExtractor>();

        _client = new SharpTubeClient(
            _youtubeClient,
            _channelExtractor,
            _playlistExtractor,
            _videoExtractor);
    }

    [Fact]
    public async Task Init_ShouldCallInitCookies()
    {
        // Act
        await _client.Init();

        // Assert
        await _youtubeClient.Received(1).InitCookies();
    }

    [Fact]
    public async Task GetChannel_ShouldReturnParsedChannel()
    {
        // Arrange
        var channelId = _fixture.Create<string>();
        var rawData = _fixture.Create<string>();
        var expectedChannel = _fixture.Create<Channel>();


        _youtubeClient.GetChannel(channelId)
            .Returns(Task.FromResult(rawData));

        _channelExtractor.Extract(rawData)
            .Returns(expectedChannel);

        // Act
        var result = await _client.GetChannel(channelId);

        // Assert
        Assert.Equal(expectedChannel, result);
    }

    [Fact]
    public async Task GetPlaylist_ShouldReturnParsedPlaylist()
    {
        // Arrange
        var playlistId = _fixture.Create<string>();
        var rawData = _fixture.Create<string>();
        var expectedPlaylist = _fixture.Create<Playlist>();

        _youtubeClient.GetPlaylist(playlistId)
            .Returns(Task.FromResult(rawData));

        _playlistExtractor.Extract(playlistId, rawData)
            .Returns(expectedPlaylist);

        // Act
        var result = await _client.GetPlaylist(playlistId);

        // Assert
        Assert.Equal(expectedPlaylist, result);
    }

    [Fact]
    public async Task GetVideo_ShouldReturnParsedVideo()
    {
        // Arrange
        var videoId = _fixture.Create<string>();
        var rawData = _fixture.Create<string>();

        _youtubeClient.GetVideo(videoId)
            .Returns(Task.FromResult(rawData));

        var expectedVideo = _fixture.Create<Video>();

        _videoExtractor.Extract(videoId, rawData)
            .Returns(expectedVideo);

        // Act
        var result = await _client.GetVideo(videoId);

        // Assert
        Assert.Equal(expectedVideo, result);
    }
}
