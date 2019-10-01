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

        public async Task YoutubeSearchAndDownloadTest()
        {
            //User would enter a search which we use to generate a Track entity.
            Console.WriteLine("Please enter a title");
            var title = Console.ReadLine();

            Console.WriteLine("Please Enter the Author");
            var author = Console.ReadLine();

            //Generate the track entity within the UI. (Set duration to 0 as we don't care about that in the useCase)
            var searchTrack = new Track(title, new[] { author }, 0);

            //Search the stream repo for which is essentially searching youtube.
            var ytTracks = await _streamRepository.SearchAsync(searchTrack);

            //Display results (At this point you have YoutubeTrack entities which have an ID associated with them)
            foreach (var track in ytTracks)
            {
                Console.WriteLine($"{track.Title} : {track.Id}");
            }

            //Here we could do a few things, in this example I am getting them to enter an ID because it's a console UI. 
            //In your UI you could have a select box or whatever, essentially you just need to be able to get the ID based on what the user selected.
            Console.WriteLine("\nPlease enter an ID from the options above.");

            var id = Console.ReadLine();

            //Now we select the track using Linq, again ensure this is by ID as it'll be the most secure way of doing it.
            var selectedTrack = ytTracks.FirstOrDefault(t => t.Id == id);

            //Pass the selected track to the download service, specify as directory as always.
            //Save filename is the same format as with spotify except now we use Youtube title and author for obvious reasons.
            await _downloadService.DownloadMediaFromYoutubeTrackAsync(selectedTrack, "Test");

            Console.WriteLine($"Download: {selectedTrack.Title} Completed.");
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
                    await _downloadService.DownloadMediaFromTrackAsync(track: track, filePath: "Music", appendLyrics: true, useAuthor: true);
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