using System.IO;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Youtube.Helpers;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace Youtube2Mp3.Youtube.Services
{
    public class YoutubeStreamRepository : IStreamRepository
    {
        private YoutubeClient _client;

        public YoutubeStreamRepository(YoutubeClient client)
        {
            _client = client;
        }

        public async Task<MemoryStream> GetStreamOfTrackAsync(Track track)
        {
            var stream = new MemoryStream();
            var video = await SearchYoutubeAsync(track);

            if (video is null || video.Id is null) { return stream; }

            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(video.Id);
            var audioStreamInfo = streamInfoSet.Audio.WithHighestBitrate();

            var mediaStream = await _client.GetMediaStreamAsync(audioStreamInfo);
            mediaStream.CopyTo(stream);

            return stream;
        }

        private async Task<Video> SearchYoutubeAsync(Track track)
        {
            var videos = await _client.SearchVideosAsync(track.Title, 1);
            var filteredResult = videos.GetByTitle(track.Title);

            return filteredResult;
        }
    }
}
