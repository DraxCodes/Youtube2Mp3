using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Text.RegularExpressions;

namespace Youtube2Mp3.Spotify.Extensions
{
    public static class SpotifyClientExtensions
    {
        private static readonly Regex spotifyPlaylistIdPattern = new Regex(@"playlist[\/|:](.{22})", RegexOptions.Compiled);

        public static FullPlaylist? GetPlaylistByUrl(this SpotifyWebAPI api, string url)
        {
            var id = ParseSpotifyIdFromUrl(url);
            return id is null
                ? null
                : api.GetPlaylist(id);
        }

        public static string? ParseSpotifyIdFromUrl(string url)
            => url is null || !spotifyPlaylistIdPattern.IsMatch(url) 
                ? null 
                : spotifyPlaylistIdPattern.Match(url).Groups[1].Value;
    }
}
