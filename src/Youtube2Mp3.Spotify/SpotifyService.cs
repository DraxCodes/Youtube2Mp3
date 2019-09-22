using System;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Extensions;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Spotify.Extensions;

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

        public async Task<SpotifyTrack[]> LoadPlaylistAsync(string url)
        {
            var playlist = _webApi.GetPlaylistByUrl(url);

            SpotifyTrack[] tracks = new SpotifyTrack[playlist.Tracks.Total];

            for (int i = 0; i < playlist.Tracks.Total; i++)
            {
                // if this takes quite a while to do, we might be better off using Johnny's models/entities.
                var currentTrack = playlist.Tracks.Items[i].Track;

                tracks[i] = new SpotifyTrack
                {
                    Title = currentTrack.Name,
                    Album = currentTrack.Album.Name,
                    Artists = currentTrack.Artists.Select(x => x.Name).ToArray(),
                    DurationMs = (uint)currentTrack.DurationMs
                };
            }

            return tracks;
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
