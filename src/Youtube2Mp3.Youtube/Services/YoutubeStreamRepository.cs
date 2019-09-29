using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
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

        public async Task<MemoryStream> GetStreamOfTrackAsync(Track track)
        {
            var stream = new MemoryStream();
            var video = await SearchYoutubeAsync(track);

            if (video is null || video.Id is null) { return stream; }

            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(video.Id);
            var audioStreamInfo = streamInfoSet.Audio.WithHighestBitrate();

            var mediaStream = await _client.GetMediaStreamAsync(audioStreamInfo);
            mediaStream.CopyTo(stream);

            return stream;
        }

        private async Task<Video> SearchYoutubeByTitleAsync(Track track, bool appendLyrics = false)
        {
            var videos = await _client.SearchVideosAsync(track.Title, 1);
            var filteredResult = videos.GetByTitle(track.Title);

            if (appendLyrics)
            {
                filteredResult = videos.GetManyByTitle(track.Title).GetByTitle("lyrics");
            }

            return filteredResult;
        }

        private async Task<Video> SearchYoutubeAsync(Track track, bool appendLyrics = false)
        {
            var videos = await _client.SearchVideosAsync($"{track.Authors.First()} - {track.Title}", 1);
            var result = videos.GetManyByTitle(track.Title);
            var filteredResult = result.GetByArtists(track.Authors);

            if (appendLyrics)
            {
                filteredResult = result.GetManyByTitle("lyrics").GetByArtists(track.Authors);
            }

            return filteredResult;
        }

        private async Task<Video> SearchYoutubeWithDurationAsync(Track track, bool appendLyrics = false)
        {
            var videos = await _client.SearchVideosAsync(track.Title, 1);
            var result = videos.GetManyByTitle(track.Title);
            var filteredResult = result.GetByClosestTime(track.Duration);

            if (appendLyrics)
            {
                filteredResult = result.GetManyByTitle("lyrics").GetByClosestTime(track.Duration);
            }

            return filteredResult;
        }

        private async Task<Video> GetBestYoutubeResultAsync(Track track, TimeSpan duration, bool appendLyrics)
        {
            var videos = await _client.SearchVideosAsync($"{track.Authors.First()} - {track.Title}", 1);
            var bestMatch = videos.GetBestMatch(duration, track.Title, track.Authors, appendLyrics);

            return bestMatch;
        }
    }
}
