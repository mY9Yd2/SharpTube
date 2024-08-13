using System.Globalization;
using System.Text;

using SharpTube.YouTube;

string channelId = "@IronMouseParty";
//string channelId = "UChgPVLjqugDQpRLWvC7zzig";
//string channelId = "ironmouseparty";

await Client.InitCookies();

Channel channel = await Channel.GetChannel(channelId);
Playlist playlist = await Playlist.GetPlaylist(channel.PlaylistId);
Video video = await Video.GetVideo(playlist.VideoIds[0]);

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
	Thumbnail: https://yt3.googleusercontent.com/ytc/AIdro_lnpcdCnJi5j9aL2TtXam65hLVm2Fb9wG6kYUyo9E-yi0E=s900-c-k-c0x00ffffff-no-rj
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
		VODS: https://youtube.com/channel/UC733wqgq7RmafDY7qEACsBg
		Patreon: https://patreon.com/ironmouse

Playlist:
	Name: ironmouse
	Url: https://www.youtube.com/playlist?list=UUhgPVLjqugDQpRLWvC7zzig
	Thumbnail: https://i.ytimg.com/vi/YqUL7qCyQug/hqdefault.jpg
	VideoCount: 625
	Id: UUhgPVLjqugDQpRLWvC7zzig
	First 5 videoIds:
		YqUL7qCyQug
		4823qX6REz4
		zSp6qd3oC2M
		K3q4UcjJenM
		2GVAuZv-dYs

Video:
	DisplayId: YqUL7qCyQug
	FullTitle: Playing Every Mobile Brainrot Game
	Duration: 898
	DurationString: 14:58
	MachineReadableDurationString: PT14M58S
	Timestamp: 2024-08-11T17:15:01 (1723396501)
	ChannelId: UChgPVLjqugDQpRLWvC7zzig
	ChannelUrl: https://www.youtube.com/channel/UChgPVLjqugDQpRLWvC7zzig
	First 3 tags:
		Ironmouse
		VTuber
		brainrot
	Thumbnail: https://i.ytimg.com/vi/YqUL7qCyQug/maxresdefault.jpg
	OriginalUrl: https://www.youtube.com/watch?v=YqUL7qCyQug

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
