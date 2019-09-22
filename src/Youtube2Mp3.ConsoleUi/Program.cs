using Microsoft.Extensions.DependencyInjection;
using Youtube2Mp3.ConsoleUi.IOC;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.ConsoleUi
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Setup.Services();
            var ui = services.GetRequiredService<IUiService>();

            ui.SendOutput("Currently a WIP");
        }
    }
}
