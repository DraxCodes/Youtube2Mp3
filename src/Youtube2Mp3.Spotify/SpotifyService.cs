using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Spotify.Entities;
using Youtube2Mp3.Spotify.Extensions;

namespace Youtube2Mp3.Spotify
{
    public class SpotifyTrackRepository : ITrackRespository
    {
        private SpotifyWebAPI _webApi;
        private SpotifyAuth _auth = new SpotifyAuth();

        public SpotifyTrackRepository(string clientId, string clientSecret)
        {
            _auth.ClientId = clientId;
            _auth.ClientSecret = clientSecret;
        }

        public async Task<IEnumerable<Track>> LoadPlaylistAsync(string url)
        {
            _webApi = await InitializeWebApi();

            var playlist = _webApi.GetPlaylistByUrl(url);
            var result = new List<Track>();

            if (playlist is null) { return result; }

            var playlistItems = playlist.Tracks.Items;

            Parallel.ForEach(playlistItems, item =>
            {
                result.Add(new Track {
                    Title = item.Track.Name,
                    Authors = item.Track.Artists.Select(a => a.Name),
                    DurationMilliSeconds = (uint)item.Track.DurationMs
                });
            });

            return result;
        }

        private async Task<SpotifyWebAPI> InitializeWebApi()
        {
            var auth = new CredentialsAuth(_auth.ClientId, _auth.ClientSecret);
            var token = await auth.GetToken();
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                UseAuth = true,
                AccessToken = token.AccessToken,
                TokenType = "Bearer"
            };

            return api;
        }
    }
}
