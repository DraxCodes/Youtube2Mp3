using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System;

namespace Youtube2Mp3.Spotify.Extensions
{
    public static class SpotifyClientExtensions
    {
        public static FullPlaylist GetPlaylistByUrl(this SpotifyWebAPI api, string url)
        {
            string userId = string.Empty;
            string playlistId = string.Empty;

            if (url.StartsWith("https://open.spotify.com"))
            {
                var parts = url.Split('/', StringSplitOptions.RemoveEmptyEntries);

                if (!url.Contains("/user/"))
                {
                    playlistId = parts[3];
                }
                else
                {
                    userId = parts[3];
                    playlistId = parts[5].Split("?si=")[0];
                }
             
            }

            else if (url.StartsWith("spotify:user:"))
            {
                var parts = url.Split(':');
                userId = parts[2];
                playlistId = parts[4];
            }

            return api.GetPlaylist(playlistId);
        }
    }
}
