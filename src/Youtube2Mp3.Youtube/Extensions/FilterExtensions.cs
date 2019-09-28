using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeExplode.Models;

namespace Youtube2Mp3.Youtube.Helpers
{
    public static class VideoFilter
    {
        public static Video GetByTitle(this IReadOnlyList<Video> videos, string title)
        {
            return videos.FirstOrDefault(v => v.Title.Contains(title));
        }

        public static IEnumerable<Video> GetManyByTitle(this IReadOnlyList<Video> videos, string title)
        {
            return videos.Where(v => v.Title == title);
        }

        public static Video GetBestMatch(this IReadOnlyList<Video> videos, string title, TimeSpan duration)
        {
            var titleResults = videos.Where(v => v.Title == title);
            var durationResult = titleResults.FirstOrDefault(v => v.Duration.TotalSeconds == duration.TotalSeconds);

            return durationResult;
        }

        public static IEnumerable<Video> GetBestMatches(this IReadOnlyList<Video> videos, string title, TimeSpan duration)
        {
            var titleResults = videos.Where(v => v.Title == title);
            var durationResults = titleResults.Where(v => v.Duration.TotalSeconds == duration.TotalSeconds);

            return durationResults;
        }
    }
}
