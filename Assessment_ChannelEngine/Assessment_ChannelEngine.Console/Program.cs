using System;
using Assessment_ChannelEngine.Console.Interface;

namespace Assessment_ChannelEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleHandler = new RegisterContainers().GetService<IConsoleHandler>();

            consoleHandler.Start();
        }
    }
}
