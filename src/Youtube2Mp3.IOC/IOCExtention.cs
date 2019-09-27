using Microsoft.Extensions.DependencyInjection;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Spotify;
using Youtube2Mp3.Youtube.Services;
using YoutubeExplode;

namespace Youtube2Mp3.IOC
{
    public static class IOCExtention
    {
        public static IServiceCollection UseSpotify(this IServiceCollection services)
            => services.AddSingleton<ITrackRespository, SpotifyTrackRepository>();

        public static IServiceCollection UseYoutube(this IServiceCollection services)
            => services.AddSingleton<IStreamRepository, YoutubeStreamRepository>()
                       .AddSingleton<IDownloadService, YoutubeDownloadService>()
                       .AddSingleton<YoutubeClient>();
    }
}
