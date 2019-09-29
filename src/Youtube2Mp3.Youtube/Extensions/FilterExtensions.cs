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
              .Select(p => p.x);

            return result;
        }

        private static Video GetByAuthor(this IEnumerable<Video> videos, IEnumerable<string> authors)
            => videos.GetManyByAuthors(authors).FirstOrDefault();

        private static IEnumerable<Video> GetManyByAuthors(this IEnumerable<Video> videos, IEnumerable<string> authors)
        {
            var filteredVideos = new List<Video>();

            // This is ugly and needs to be refactored.
            foreach (var video in videos)
            {
                foreach (var author in authors)
                {
                    if (video.Title.Contains(author) && !filteredVideos.Contains(video))
                    {
                        filteredVideos.Add(video);
                    }
                }
            }

            return filteredVideos;
        }

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
            var artistsResults = durationResults?.GetManyByAuthors(artists);

            return artistsResults;
        }
    }
}
