using Microsoft.Extensions.DependencyInjection;
using Youtube2Mp3.IOC;
using System;

namespace Youtube2Mp3.Xamarin.IOC
{
    public static class Setup
    {
        public static IServiceProvider Services()
            => new ServiceCollection()
            .UseYoutube()
            .UseSpotify()
            .BuildServiceProvider();
    }
}
