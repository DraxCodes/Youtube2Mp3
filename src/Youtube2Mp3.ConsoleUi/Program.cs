using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Youtube2Mp3.ConsoleUi.IOC;
using Youtube2Mp3.ConsoleUi.Services;

namespace Youtube2Mp3.ConsoleUi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = Setup.Services();
            var ytClient = services.GetRequiredService<YoutubeUI>();
            await ytClient.Test();
        }
    }
}
