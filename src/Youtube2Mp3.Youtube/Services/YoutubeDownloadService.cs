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
    public class YoutubeDownloadService : IDownloadService
    {
        private readonly YoutubeClient _client;
        private readonly IStreamRepository _streamRepository;

        public YoutubeDownloadService(YoutubeClient client, IStreamRepository streamRepository)
        {
            _client = client;
            _streamRepository = streamRepository;
        }

        public async Task DownloadMediaAsync(Track track, string filePath)
        {
            var videoStream = await _streamRepository.GetStreamOfTrackAsync(track);
            var bytes = videoStream.ToArray();

            File.WriteAllBytes($"{filePath}/{track.Title}.mp3", bytes);
        }

        private async Task<Video> SearchYoutubeAsync(Track track)
        {
            var videos = await _client.SearchVideosAsync(track.Title);
            var filteredResult = VideoFilter.BestMatchSingle(videos, track.Title, track.Duration);

            return filteredResult;
        }
    }
}
