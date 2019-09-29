using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeExplode.Models;

namespace Youtube2Mp3.Youtube.Helpers
{
    public static class VideoFilter
    {
        public static Video GetByTitle(this IEnumerable<Video> videos, string title)
            => videos?.GetManyByTitle(title).FirstOrDefault();

        public static IEnumerable<Video> GetManyByTitle(this IEnumerable<Video> videos, string title)
            => videos?.Where(v => v.Title.Contains(title));

        public static Video GetByClosestTime(this IEnumerable<Video> videos, TimeSpan duration)
            => videos?.GetManyByClosestTime(duration).FirstOrDefault();

        public static IEnumerable<Video> GetManyByClosestTime(this IEnumerable<Video> videos, TimeSpan duration)
        {
            var within = 10;
            var result = videos?.Select(x => new { x, distance = Math.Abs(x.Duration.TotalSeconds - duration.TotalSeconds) })
              .Where(p => p.distance <= within)
              .OrderBy(p => p.distance)
              .Select(p => p.x);

            return result;
        }

        public static Video GetByArtists(this IEnumerable<Video> videos, IEnumerable<string> authors)
            => videos.GetManyByArtists(authors).FirstOrDefault();

        public static IEnumerable<Video> GetManyByArtists(this IEnumerable<Video> videos, IEnumerable<string> authors)
            => videos?.SelectMany(video => authors?.Where(author => video.Title.Contains(author)).Select(author => video));

        public static Video GetBestMatch(this IEnumerable<Video> videos, TimeSpan duration,
            string title, IEnumerable<string> artists, bool appendLyrics = false)
        {
            return videos.GetBestMatches(duration, title, artists, appendLyrics).FirstOrDefault();
        }

        public static IEnumerable<Video> GetBestMatches(this IEnumerable<Video> videos, TimeSpan duration, 
            string title, IEnumerable<string> artists, bool appendLyrics = true)
        {
            var titleResults = videos?.GetManyByTitle(title);
            if (appendLyrics) { titleResults = titleResults?.GetManyByTitle("lyrics"); }

            var durationResults = titleResults?.GetManyByClosestTime(duration);
            var artistsResults = durationResults?.GetManyByArtists(artists);

            return artistsResults;
        }
    }
}
