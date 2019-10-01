using System;
using System.Collections.Generic;
using System.Text;

namespace Youtube2Mp3.Core.Entities
{
    public class YoutubeTrack : Track
    {
        public string Id { get; private set; }
        public YoutubeTrack(string title, IEnumerable<string> authors, int durationMilliSeconds, string id) : base(title, authors, durationMilliSeconds)
        {
            Id = id;
        }
    }
}
