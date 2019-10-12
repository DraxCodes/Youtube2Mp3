using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Text.RegularExpressions;

namespace Youtube2Mp3.Spotify.Extensions
{
    public static class SpotifyClientExtensions
    {
        private static Regex spotifyPlaylistIdPattern = new Regex(@"playlist[\/|:](.{22})", RegexOptions.Compiled);

        public static FullPlaylist? GetPlaylistByUrl(this SpotifyWebAPI api, string url)
        {
            var id = ParseSpotifyIdFromUrl(url);
            if (id is null) { return null; }

            return api.GetPlaylist(id);
        }

        public static string? ParseSpotifyIdFromUrl(string url)
        {
            if (url is null || !spotifyPlaylistIdPattern.IsMatch(url)) { return null; }
            return spotifyPlaylistIdPattern.Match(url).Groups[1].Value;
        }
    }
}
