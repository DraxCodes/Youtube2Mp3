using System;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.Spotify
{
    public class SpotifyService : ISpotifyService
    {
        private SpotifyWebAPI _webApi;
        private SpotifyAuth _auth;

        public async Task Initialize(string clientId, string clientSecret)
        {
            _webApi = await InitializeWebApi();
            _auth.ClientId = clientId;
            _auth.ClientSecret = clientSecret;
        }

        public async Task<SpotifyTrack[]> LoadPlaylistAsync(string url, int count = 20)
        {
            //https://open.spotify.com/user/{USERID}/playlist/{PLAYLISTID}?si=83igUVjWQUODOlikDxOkhQ

            string userId = "";
            string playlistId = "";
            SpotifyTrack[] tracks = new SpotifyTrack[count];

            var userPlaylists = await _webApi.GetUserPlaylistsAsync(userId, count);
            var playlist = userPlaylists.Items.AsEnumerable();

            var test = playlist.First(t => t.Id == playlistId);


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
