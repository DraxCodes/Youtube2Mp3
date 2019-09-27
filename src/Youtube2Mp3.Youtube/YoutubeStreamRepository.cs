using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace Youtube2Mp3.Youtube
{
    public class YoutubeStreamRepository : IStreamRepository
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
            var trackId = await FetchYoutubeIdAsync(track);

            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(trackId);
            var streamInfo = streamInfoSet.Audio.WithHighestBitrate();

            var mediaStream = await _client.GetMediaStreamAsync(streamInfo);
            mediaStream.CopyTo(stream);

            return stream;
        }

        private async Task<string> FetchYoutubeIdAsync(Track track)
        {
            var result = string.Empty;
            var trackResults = await SearchYoutubeAsync(track.Title);

            var filteredSearchResult = FilterYoutubeResults(trackResults, track.Title, track.Duration);
            
            result = filteredSearchResult.Id;

            return result;
        }

        private async Task<IReadOnlyList<Video>> SearchYoutubeAsync(string title)
        {
            return await _client.SearchVideosAsync(title);
        }

        private Video FilterYoutubeResults(IReadOnlyList<Video> videos, string title, TimeSpan duration)
        {
            var titleResults = videos.Where(v => v.Title == title);
            var durationResult = titleResults.FirstOrDefault(v => v.Duration.TotalSeconds == duration.TotalSeconds);

            return durationResult;
        }
    }
}
