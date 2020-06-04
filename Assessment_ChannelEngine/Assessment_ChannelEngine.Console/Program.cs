using Assessment_ChannelEngine.Console.Interface;

namespace Assessment_ChannelEngine.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var consoleHandler = new RegisterContainers().GetService<IConsoleHandler>();

            consoleHandler.Start();
        }
    }
}