using System;
using System.Collections.Generic;
using System.Linq;
using Youtube2Mp3.Core.Entities;
using YoutubeExplode.Models;

namespace Youtube2Mp3.Youtube.Helpers
{
    public static class VideoFilter
    {
        public static Video FilterClosestTime(this IEnumerable<Video> videos, Track track, TimeSpan duration)
        {
            var results = videos.Where(v => v.Duration.TotalSeconds % track.Duration.TotalSeconds < duration.TotalSeconds);
            return results.FirstOrDefault();
        }

        public static IEnumerable<Video> FilterManyClosestTime(this IEnumerable<Video> videos, Track track, TimeSpan duration)
        {
            var results = videos.Where(v => v.Duration.TotalSeconds % track.Duration.TotalSeconds < duration.TotalSeconds);
            return results;
        }
    }
}
