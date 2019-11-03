using System.Collections.Generic;

namespace Youtube2Mp3.Core.Entities
{
    public class YoutubeTrack : Track, ITrack
    {
        public string Id { get; private set; }

        public YoutubeTrack(string title, IEnumerable<string> authors, int durationMilliSeconds, string id) : base(title, authors, durationMilliSeconds)
        {
            Id = id;
        }
    }
}
