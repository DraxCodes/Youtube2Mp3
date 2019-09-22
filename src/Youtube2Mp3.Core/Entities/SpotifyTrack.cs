namespace Youtube2Mp3.Core.Entities
{
    public class SpotifyTrack
    {
        public string Title { get; set; }
        public string[] Artists { get; set; }
        public uint DurationMs { get; set; }
        public string Album { get; set; }
    }
}
