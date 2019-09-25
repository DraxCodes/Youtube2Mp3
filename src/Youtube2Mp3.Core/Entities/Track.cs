using System.Collections.Generic;

namespace Youtube2Mp3.Core.Entities
{
    public class Track
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public uint DurationMilliSeconds { get; set; }
    }
}
