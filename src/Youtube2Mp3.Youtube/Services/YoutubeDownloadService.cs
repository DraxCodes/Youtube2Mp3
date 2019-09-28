using System;
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
            EnsurePathExists(filePath);

            var stream = await _streamRepository.GetStreamOfTrackAsync(track);
            var streamArray = stream.ToArray();

            if (stream.Length < 1)
            {
                Console.WriteLine($"Track not found! {track.Title}");
                return;
            }

            File.WriteAllBytes($"{filePath}/{track.Title}.mp3", streamArray);
        }

        private void EnsurePathExists(string filePath)
        {
            if (Directory.Exists(filePath)) { return; }

            Directory.CreateDirectory(filePath);
        }
    }
}
