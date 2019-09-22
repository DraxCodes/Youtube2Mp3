using System;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.ConsoleUi.Services
{
    public class UiService : IUiService
    {
        public string GetInput()
            => Console.ReadLine();

        public void SendOutput(string line)
            => Console.WriteLine(line);
    }
}
