using System;

namespace Youtube2Mp3.Core.Extensions
{
    public static class StringExtensions
    {
        public static string[] GetUserAndPlaylistId(this string url)
        {
            string userId = string.Empty;
            string playlistId = string.Empty;

            if (url.StartsWith("https://open.spotify.com"))
            {
                if (!url.Contains("/user/")) { throw new NotImplementedException(); }

                var parts = url.Split('/', StringSplitOptions.RemoveEmptyEntries);
                userId = parts[3];
                playlistId = parts[5].Split("?si=")[0];
            }

            else if (url.StartsWith("spotify:user:"))
            {
                var parts = url.Split(':');
                userId = parts[2];
                playlistId = parts[4];
            }

            return new string[] { userId, playlistId };
        }
    }
}
