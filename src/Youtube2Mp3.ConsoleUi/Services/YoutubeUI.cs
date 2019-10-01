using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.ConsoleUi.Services
{
    public class YoutubeUI
    {
        private readonly ITrackRespository _trackRespository;
        private readonly IDownloadService _downloadService;
        private readonly IStreamRepository _streamRepository;

        public YoutubeUI(ITrackRespository trackRespository, IDownloadService downloadService, IStreamRepository streamRepository)
        {
            _trackRespository = trackRespository;
            _downloadService = downloadService;
            _trackRespository.InitializeSpotifyAuth("DO NOT POST", "THIS INFO TO GITHUB");
        }

        public async Task SearchYoutubeTest()
        {
            var searchTrack = new Track("SuperNova", new[] { "Mr Hudson" }, 0);
            var results = await _streamRepository.SearchAsync(searchTrack);

            foreach (var result in results)
            {
                Console.WriteLine($"{result.Authors.First()} - {result.Title} :: {result.Duration.ToString(@"mm\:ss")}");
            }
        }

        public async Task SpotifyPlaylistYoutubeDownloadTest()
        {
            Console.WriteLine("Attempting....");
            var tracks = await _trackRespository.LoadPlaylistAsync("https://open.spotify.com/playlist/0apX36HEcBc4qRsPoZcdRQ");
            var tracksToDl = tracks.OrderBy(t => t.Title);
            var timer = new Stopwatch();

            int i = 1;

            try
            {
                timer.Start();
                foreach (var track in tracksToDl)
                {
                    await _downloadService.DownloadMediaAsync(track: track, filePath: "Music", appendLyrics: true, useAuthor: true);
                    Console.WriteLine($"[{i}]     {track.Title} Downloaded.");
                    i++;
                }
                timer.Stop();
                
                Console.WriteLine("Yay!");
                Console.WriteLine($"Time Taken: {timer.Elapsed.TotalSeconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}