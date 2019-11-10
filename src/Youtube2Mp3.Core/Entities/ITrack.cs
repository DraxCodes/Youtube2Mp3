using System;
using System.Collections.Generic;

namespace Youtube2Mp3.Core.Entities
{
    public interface ITrack
    {
        public string Title { get; }
        public IEnumerable<string> Authors { get; }
        public TimeSpan Duration { get; }
    }
}
