using System.Globalization;
using System.Text;

using SharpTube;
using SharpTube.YouTube;

string channelId = "@IronMouseParty";
//string channelId = "UChgPVLjqugDQpRLWvC7zzig";
//string channelId = "ironmouseparty";

ISharpTubeClient sharpTube = new SharpTubeClient();

await sharpTube.Init();

Channel channel = await sharpTube.GetChannel(channelId);
Playlist playlist = await sharpTube.GetPlaylist(channel.PlaylistId);
Video video = await sharpTube.GetVideo(playlist.VideoIds[0]);

Console.WriteLine(FormatChannelInfo(channel));
Console.WriteLine(FormatPlaylistInfo(playlist));
Console.WriteLine(FormatVideoInfo(video));

/*

Example output:

Channel:
    PlaylistId: UUhgPVLjqugDQpRLWvC7zzig
    Uploader: ironmouse
    UploaderId: @IronMouseParty
    UploaderUrl: https://www.youtube.com/@IronMouseParty
    Thumbnail: https://yt3.googleusercontent.com/ytc/AIdro_kRmBUGJyEGZ46bHqlcqAo-yEntsTm2c0vmDzQb3jck978=s900-c-k-c0x00ffffff-no-rj
    ChannelId: UChgPVLjqugDQpRLWvC7zzig
    ChannelUrl: https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig
    Tags:
        ironmouse
        VTuber
        vshojo
        twitch
        stream
        mouse
        tungsten rat
        mousy
        virtual
    ExternalLinks:
        Twitter: https://twitter.com/ironmouse
        Twitch: https://twitch.tv/ironmouse
        TikTok: https://tiktok.com/@ironmouse
        Clips: https://youtube.com/@MouseClipsOfficial
        VODS: https://youtube.com/channel/UC733wqgq7RmafDY7qEACsBg
        Patreon: https://patreon.com/ironmouse

Playlist:
    Name: ironmouse
    Url: https://www.youtube.com/playlist?list=UUhgPVLjqugDQpRLWvC7zzig
    Thumbnail: https://i.ytimg.com/vi/dBxfhXVncmA/hqdefault.jpg
    VideoCount: 914
    Id: UUhgPVLjqugDQpRLWvC7zzig
    First 5 videoIds:
            dBxfhXVncmA
            joTr74Mw5_g
            kPv-20P3afE
            o4D0CS197rY
            QUXnEKE-R1Y

Video:
    DisplayId: dBxfhXVncmA
    FullTitle: Ironmouse's Mom Tries Protecting Connor..
    Duration: 32
    DurationString: 32
    MachineReadableDurationString: PT32S
    Timestamp: 2025-05-14T06:53:13 (1747205593)
    ChannelId: UChgPVLjqugDQpRLWvC7zzig
    ChannelUrl: https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig
    First 3 tags:
            Ironmouse
            VTuber
    Thumbnail: https://i.ytimg.com/vi/dBxfhXVncmA/maxres2.jpg?sqp=-oaymwEoCIAKENAF8quKqQMcGADwAQH4AbYIgAKAD4oCDAgAEAEYZSBQKFQwDw==\u0026rs=AOn4CLBBdkvmTFlA3ceSVBulLJTDs1yQlg
    OriginalUrl: https://www.youtube.com/watch?v=dBxfhXVncmA

*/

string FormatChannelInfo(Channel channel)
{
    StringBuilder strBuilder = new();

    strBuilder
        .AppendLine("Channel:")
        .AppendLine($"\tPlaylistId: {channel.PlaylistId}")
        .AppendLine($"\tUploader: {channel.Uploader}")
        .AppendLine($"\tUploaderId: {channel.UploaderId}")
        .AppendLine($"\tUploaderUrl: {channel.UploaderUrl}")
        .AppendLine($"\tThumbnail: {channel.Thumbnail}")
        .AppendLine($"\tChannelId: {channel.ChannelId}")
        .AppendLine($"\tChannelUrl: {channel.ChannelUrl}")
        .AppendLine($"\tTags:\n\t\t{string.Join("\n\t\t", channel.Tags)}")
        .AppendLine($"\tExternalLinks:");
    foreach (var link in channel.ExternalLinks)
    {
        strBuilder.AppendLine($"\t\t{link.Key}: {link.Value}");
    }

    return strBuilder.ToString();
}

string FormatPlaylistInfo(Playlist playlist)
{
    StringBuilder strBuilder = new();

    strBuilder
        .AppendLine("Playlist:")
        .AppendLine($"\tName: {playlist.Name}")
        .AppendLine($"\tUrl: {playlist.Url}")
        .AppendLine($"\tThumbnail: {playlist.Thumbnail}")
        .AppendLine($"\tVideoCount: {playlist.VideoCount}")
        .AppendLine($"\tId: {playlist.Id}")
        .AppendLine($"\tFirst 5 videoIds:\n\t\t{string.Join("\n\t\t", playlist.VideoIds.Take(5))}");

    return strBuilder.ToString();
}

string FormatVideoInfo(Video video)
{
    StringBuilder strBuilder = new();

    strBuilder
        .AppendLine("Video:")
        .AppendLine($"\tDisplayId: {video.DisplayId}")
        .AppendLine($"\tFullTitle: {video.FullTitle}")
        .AppendLine($"\tDuration: {video.Duration}");

    string dateTimeFormated = video.Timestamp.UtcDateTime.ToString("s", CultureInfo.InvariantCulture);

    strBuilder
        .AppendLine($"\tDurationString: {video.DurationString}")
        .AppendLine($"\tMachineReadableDurationString: {video.MachineReadableDurationString}")
        .AppendLine($"\tTimestamp: {dateTimeFormated} ({video.Timestamp.ToUnixTimeSeconds()})")
        .AppendLine($"\tChannelId: {video.ChannelId}")
        .AppendLine($"\tChannelUrl: {video.ChannelUrl}")
        .AppendLine($"\tFirst 3 tags:\n\t\t{string.Join("\n\t\t", video.Tags.Take(3))}")
        .AppendLine($"\tThumbnail: {video.Thumbnail}")
        .AppendLine($"\tOriginalUrl: {video.OriginalUrl}");

    return strBuilder.ToString();
}
