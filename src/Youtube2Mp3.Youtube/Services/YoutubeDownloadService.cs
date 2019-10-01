using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Extensions;
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

        public async Task DownloadMediaAsync(Track track, string filePath, bool appendLyrics, bool useAuthor)
        {
            EnsurePathExists(filePath);

            var stream = await _streamRepository.GetStreamOfTrackAsync(track, appendLyrics, useAuthor);
            var streamArray = stream.ToArray();

            if (stream.Length < 1)
            {
                Console.WriteLine($"Track not found! {track.Title}");
                return;
            }

            var trackSaveName = track.FormatSafeTrackName();

            File.WriteAllBytes($"{filePath}/{trackSaveName}.mp3", streamArray);

            await stream.FlushAsync();
        }

        private void EnsurePathExists(string filePath)
        {
            if (Directory.Exists(filePath)) { return; }

            Directory.CreateDirectory(filePath);
        }
    }
}
