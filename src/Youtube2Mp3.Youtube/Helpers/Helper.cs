using YoutubeExplode.Models;

namespace Youtube2Mp3.Youtube.Helpers
{
    public static class Helper
    {
        public static string GetVideoId(Video video)
        {
            return video.Id;
        }
    }
}
