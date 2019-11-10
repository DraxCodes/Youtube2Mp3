using System;
using System.IO;
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

        public async Task DownloadMediaFromYoutubeTrackAsync(YoutubeTrack track, string filePath)
        {
            EnsurePathExists(filePath);

            var stream = await _streamRepository.GetStreamByYoutubeTrackAsync(track);
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

        public async Task DownloadMediaFromTrackAsync(ITrack track, string filePath, bool appendLyrics, bool useAuthor, bool allowFallback = true)
        {
            EnsurePathExists(filePath);

            var stream = await _streamRepository.GetStreamByTrackAsync(track, appendLyrics, useAuthor, allowFallback);
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
