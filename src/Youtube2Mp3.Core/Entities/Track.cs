using System;
using System.Collections.Generic;

namespace Youtube2Mp3.Core.Entities
{
    public class Track
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; private set; }
        public TimeSpan Duration { get; private set; }

        public Track(string title, IEnumerable<string> authors, int durationMilliSeconds)
        {
            Title = title;
            Authors = authors;
            Duration = GenerateTimeSpan(durationMilliSeconds);
        }

        public Track(string title, string author, int durationMilliSeconds)
        {
            Title = title;
            Authors = new[] { author };
            Duration = GenerateTimeSpan(durationMilliSeconds);
        }

        private TimeSpan GenerateTimeSpan(int durationMilliSeconds)
            => TimeSpan.FromMilliseconds(durationMilliSeconds);
    }
}
