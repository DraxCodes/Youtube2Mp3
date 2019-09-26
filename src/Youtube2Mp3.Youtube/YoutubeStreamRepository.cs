using System;
using System.IO;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace Youtube2Mp3.Youtube
{
    public class YoutubeStreamRepository : IStreamRepository, IDownloadService
    {
        private YoutubeClient _client;

        public YoutubeStreamRepository()
        {
            if (_client == null)
            {
                _client = new YoutubeClient();
            }
        }

        public async Task<Stream> GetStreamOfTrack(Track track)
        {
            var stream = new MemoryStream();

            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync("");
            var streamInfo = streamInfoSet.Audio.WithHighestBitrate();

            var mediaStream = await _client.GetMediaStreamAsync(streamInfo);
            mediaStream.CopyTo(stream);

            return stream;
        }

        public void DownloadMedia(Stream stream)
        {
            /*using (var progress = new ProgressBar())
            {
                _client.DownloadMediaStreamAsync(stream, $"{fileName}.mp3", progress);
            }*/
            throw new NotImplementedException();
        }
    }
}
