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

        public YoutubeUI(ITrackRespository trackRespository, IDownloadService downloadService)
        {
            _trackRespository = trackRespository;
            _downloadService = downloadService;
            // _trackRespository.InitializeSpotifyAuth();
        }

        public async Task Test()
        {
            System.Console.WriteLine("Attempting....");
            //var tracks = await _trackRespository.LoadPlaylistAsync("https://open.spotify.com/playlist/0apX36HEcBc4qRsPoZcdRQ");
            //var track = tracks.FirstOrDefault(t => t.Title.ToLower() == "spectrum");
            var track = new Track("Paradise City", new [] {"Guns n Roses"}, 387000);
            await _downloadService.DownloadMediaAsync(track, "./");
            System.Console.WriteLine("Yay!");
        }
    }
}