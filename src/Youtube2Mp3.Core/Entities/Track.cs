using System;
using System.Collections.Generic;

namespace Youtube2Mp3.Core.Entities
{
    public class Track
    {
        public string Title { get; private set; }
        public IEnumerable<string> Authors { get; private set; }
        public TimeSpan Duration { get; private set; }

        public Track(string title, IEnumerable<string> authors, uint durationMilliSeconds)
        {
            Title = title;
            Authors = authors;
            Duration = GeneratTimeSpan(durationMilliSeconds);
        }

        private TimeSpan GeneratTimeSpan(uint durationMilliSeconds)
        {
            return TimeSpan.FromMilliseconds(durationMilliSeconds);
        }
    }
}
