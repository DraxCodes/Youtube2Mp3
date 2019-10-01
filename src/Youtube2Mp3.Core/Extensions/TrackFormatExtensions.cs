using System.Linq;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Extensions
{
    public static class TrackFormatExtensions
    {
        public static string QueryFormat(this Track track, bool shouldUseAuthor, bool shouldUseLyrics)
        {
            if (shouldUseAuthor && !shouldUseLyrics) { return $"{track.Authors.First()} - {track.Title}"; }
            if (shouldUseLyrics && !shouldUseAuthor) { return $"{track.Title} lyrics"; }
            if (shouldUseAuthor && shouldUseLyrics) { return $"{track.Authors.First()} - {track.Title} lyrics"; }
            return track.Title;
        }

        public static string FormatSafeTrackName(this Track track)
        {
            var author = track.Authors.FirstOrDefault();

            if (string.IsNullOrEmpty(author))
                return $"{track.Title}".RemoveIllegalPathCharacters();
            else
                return $"{author} - {track.Title}".RemoveIllegalPathCharacters();
        }
    }
}
