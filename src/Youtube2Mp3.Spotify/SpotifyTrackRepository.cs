using System;
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
        private SpotifyWebAPI? _webApi;
        private readonly SpotifyAuth _auth = new SpotifyAuth();

        public void InitializeSpotifyAuth(string clientId, string clientSecret)
        {
            _auth.ClientId = clientId;
            _auth.ClientSecret = clientSecret;
        }

        public void InitializeSpotifyConnection(string clientId, string clientSecret)
        {
            _auth.ClientId = clientId;
            _auth.ClientSecret = clientSecret;
        }

        public async Task<IEnumerable<Track>> LoadPlaylistAsync(string url)
        {
            if (_webApi == null) { _webApi = await InitializeWebApi(); }

            try
            {
                var playlist = _webApi.GetPlaylistByUrl(url);
                var result = new List<Track>();

                if (playlist is null) { return result; }

                var playlistItems = playlist.Tracks.Items;

                foreach (var item in playlistItems)
                {
                    var artists = item.Track.Artists.Select(a => a.Name);
                    result.Add(new Track(item.Track.Name, artists, item.Track.DurationMs));
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception("Playlist not found or auth failed. Please ensure your Spotify Client ID & Secret are set. Also Ensure the playlist URL is valid.");
            }

        }

        private async Task<SpotifyWebAPI> InitializeWebApi()
        {
            var auth = new CredentialsAuth(_auth.ClientId, _auth.ClientSecret);
            var token = await auth.GetToken();
            return new SpotifyWebAPI
            {
                UseAuth = true,
                AccessToken = token.AccessToken,
                TokenType = "Bearer"
            };
        }
    }
}
