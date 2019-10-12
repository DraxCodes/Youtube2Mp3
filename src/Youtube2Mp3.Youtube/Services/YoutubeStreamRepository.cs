using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Core.Extensions;
using Youtube2Mp3.Youtube.Extensions;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Youtube2Mp3.Youtube.Services
{
    public class YoutubeStreamRepository : IStreamRepository
    {
        private readonly YoutubeClient _client;

        public YoutubeStreamRepository(YoutubeClient client)
        {
            _client = client;
        }

        public async Task<MemoryStream> GetStreamByYoutubeTrackAsync(YoutubeTrack track)
        {
            var stream = new MemoryStream();
            var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(track.Id);
            var audioStreamInfo = streamInfoSet.Audio.WithHighestBitrate();

            if (audioStreamInfo is null) { return stream; }

            try
            {
                var mediaStream = await _client.GetMediaStreamAsync(audioStreamInfo);
                await mediaStream.CopyToAsync(stream);
            }
            catch (Exception)
            {
                Console.WriteLine($"Youtube Error... {track.Title}");
                return stream;
            }

            return stream;
        }

        public async Task<MemoryStream> GetStreamByTrackAsync(Track track, bool appendLyrics, bool useAuthor, bool shouldFallBack)
        {
            var stream = new MemoryStream();
            var video = await SearchYoutubeAsync(track, appendLyrics, useAuthor, shouldFallBack);

            if (video?.Id is null) { return stream; }

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

        public async Task<IEnumerable<YoutubeTrack>> SearchAsync(Track track)
        {
            var videos = await _client.SearchVideosAsync($"{track.Authors.FirstOrDefault()} - {track.Title}", 1);
            var tracks = new Collection<YoutubeTrack>();

            foreach (var video in videos)
            {
               tracks.Add(ConvertTrack(video));
            }

            return tracks;
        }

        private YoutubeTrack ConvertTrack(Video video)
            => new YoutubeTrack(video.Title, new[] { video.Author }, (int)video.Duration.TotalMilliseconds, video.Id);

        private async Task<Video> SearchYoutubeAsync(Track track, bool shouldUseLyrics, bool shouldUseAuthor, bool shouldFallback)
        {
            string ytQuery = track.QueryFormat(shouldUseAuthor, shouldUseLyrics);

            var videos = await _client.SearchVideosAsync(ytQuery, 1);

            var durationFilter = TimeSpan.FromSeconds(30);
            var videoFilteredByDuration = videos?.FilterClosestTime(track, durationFilter);

            return videoFilteredByDuration is null && shouldFallback ? videos.FirstOrDefault() : videoFilteredByDuration;
        }
    }
}
