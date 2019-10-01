using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.ConsoleUi.Services
{
    public class YoutubeUI
    {
        private readonly ITrackRespository _trackRespository;
        private readonly IDownloadService _downloadService;

        public YoutubeUI(ITrackRespository trackRespository, IDownloadService downloadService)
        {
            _trackRespository = trackRespository;
            _downloadService = downloadService;
            _trackRespository.InitializeSpotifyAuth("DO NOT POST", "THIS INFO TO GITHUB");
        }

        public async Task Test()
        {
            Console.WriteLine("Attempting....");
            var tracks = await _trackRespository.LoadPlaylistAsync("https://open.spotify.com/playlist/6L4Q1YP0TiStXFvxjwPFvi");
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