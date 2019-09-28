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
            var tracks = await _trackRespository.LoadPlaylistAsync("https://open.spotify.com/playlist/0apX36HEcBc4qRsPoZcdRQ");
            var tracksToDl = tracks.OrderBy(t => t.Title);
            var timer = new Stopwatch();
            int i = 1;
            try
            {
                timer.Start();
                foreach (var item in tracksToDl)
                {
                    await _downloadService.DownloadMediaAsync(item, "Music");
                    Console.WriteLine($"{i}{item.Title} Downloaded.");
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