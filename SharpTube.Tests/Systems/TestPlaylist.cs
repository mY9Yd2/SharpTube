using SharpTube.YouTube;

namespace SharpTube.Tests.Systems;

public class TestPlaylist
{
    [Fact]
    public async Task GetPlaylist_ReturnsExpectedMetadata_ForValidPlaylistId()
    {
        // Arrange
        string playlistId = "UUhgPVLjqugDQpRLWvC7zzig";

        // Act
        await Client.InitCookies();
        Playlist playlist = await Playlist.GetPlaylist(playlistId);

        // Assert
        Assert.Equal("ironmouse", playlist.Name);
        Assert.Equal($"https://www.youtube.com/playlist?list={playlistId}", playlist.Url.ToString());
        Assert.False(string.IsNullOrEmpty(playlist.Thumbnail?.ToString()));
        Assert.Equal(playlistId, playlist.Id);
        Assert.True(playlist.VideoCount >= 0);
        Assert.NotNull(playlist.VideoIds);
    }
}
