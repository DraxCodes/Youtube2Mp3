using Microsoft.Extensions.DependencyInjection;
using Youtube2Mp3.ConsoleUi.Services;
using Youtube2Mp3.Core.Services;
using Youtube2Mp3.IOC;
using System;

namespace Youtube2Mp3.ConsoleUi.IOC
{
    public static class Setup
    {
        public static IServiceProvider Services()
            => new ServiceCollection()
            .AddSingleton<YoutubeUI>()
            .UseSpotify()
            .UseYoutube()
            .BuildServiceProvider();
    }
}
