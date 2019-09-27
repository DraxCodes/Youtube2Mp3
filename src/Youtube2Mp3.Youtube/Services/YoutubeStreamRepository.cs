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

        public async Task<Stream> GetStreamOfTrack(Track track)
        {
            var stream = new MemoryStream();
            var video = await SearchYoutubeAsync(track);
            var trackId = Helper.GetVideoId(video);

            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(trackId);
            var audioStreamInfo = streamInfoSet.Audio.WithHighestBitrate();

            var mediaStream = await _client.GetMediaStreamAsync(audioStreamInfo);
            mediaStream.CopyTo(stream);

            return stream;
        }

        private async Task<Video> SearchYoutubeAsync(Track track)
        {
            var videos = await _client.SearchVideosAsync(track.Title);
            var filteredResult = VideoFilter.BestMatchSingle(videos, track.Title, track.Duration);

            return filteredResult;
        }
    }
}
