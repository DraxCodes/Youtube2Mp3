using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Core.Extensions;
using Youtube2Mp3.Youtube.Helpers;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace Youtube2Mp3.Youtube.Services
{
    public class YoutubeStreamRepository : IStreamRepository
    {
        private YoutubeClient _client;

        public YoutubeStreamRepository(YoutubeClient client)
        {
            _client = client;
        }

        public async Task<MemoryStream> GetStreamOfTrackAsync(Track track, bool appendLyrics, bool useAuthor)
        {
            var stream = new MemoryStream();
            var video = await SearchYoutubeAsync(track, appendLyrics, useAuthor);

            if (video is null || video.Id is null) { return stream; }

            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(video.Id);
            var audioStreamInfo = streamInfoSet.Audio.WithHighestBitrate();

            if (audioStreamInfo is null) { return stream; }

            try
            {
                var mediaStream = await _client.GetMediaStreamAsync(audioStreamInfo);
                await mediaStream.CopyToAsync(stream);
            }
            catch (Exception)
            {
                Console.WriteLine($"Youtube Error.. {video.Title}");
                return stream;
            }
            

            return stream;
        }

        private async Task<Video> SearchYoutubeAsync(Track track, bool shouldUseLyrics, bool shouldUseAuthor)
        {
            string ytQuery = track.QueryFormat(shouldUseAuthor, shouldUseLyrics);

            var videos = await _client.SearchVideosAsync(ytQuery, 1);

            var durationFilter = TimeSpan.FromSeconds(30);
            var videoFilteredByDuration = videos.FilterClosestTime(track, durationFilter);

            if (videoFilteredByDuration is null) { return videos.FirstOrDefault(); }

            return videoFilteredByDuration;
        }
    }
}
