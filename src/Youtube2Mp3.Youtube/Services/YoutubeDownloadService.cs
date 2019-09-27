using System.IO;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.Youtube.Services
{
    public class YoutubeDownloadService : IDownloadService
    {
        private readonly IStreamRepository _streamRepository;

        public YoutubeDownloadService(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        public async Task DownloadMediaAsync(Track track, string filePath)
        {
            var videoStream = await _streamRepository.GetStreamOfTrackAsync(track);
            var bytes = videoStream.ToArray();

            File.WriteAllBytes($"{filePath}/{track.Title}.mp3", bytes);
        }
    }
}
