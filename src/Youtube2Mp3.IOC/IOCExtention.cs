using Microsoft.Extensions.DependencyInjection;
using System;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.Spotify;

namespace Youtube2Mp3.IOC
{
    public static class IOCExtention
    {
        public static IServiceCollection UseSpotify(this IServiceCollection services)
            => services.AddSingleton<ISpotifyService, SpotifyService>();

        public static IServiceCollection UseYoutube(this IServiceCollection services)
            => throw new NotImplementedException();
    }
}
