using System;
using System.Net;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.Youtube.Services
{
    public class YoutubeThumbnailRepository : IThumbnailRepository
    {
        public async Task<string> DownloadThumbnailAndGetFilePathAsync(ITrack track)
        {
            if (!(track is YoutubeTrack ytTrack)) { return string.Empty; }

            var filePath = GenerateFilePath(ytTrack);
            await DownloadThumbnailAsync(ytTrack, filePath);
            return filePath;
        }

        private async Task DownloadThumbnailAsync(YoutubeTrack track, string filePath)
        {
            using (var client = new WebClient())
            {
                var uri = new Uri($"https://img.youtube.com/vi/{track.Id}/maxresdefault.jpg");
                client.DownloadFileAsync(uri, filePath);
            }

            await Task.CompletedTask;
        }

        private string GenerateFilePath(YoutubeTrack track)
            => $"Thumbnails/{track.Title}-{track.Authors.GetEnumerator().Current}.jpg";
    }
}
