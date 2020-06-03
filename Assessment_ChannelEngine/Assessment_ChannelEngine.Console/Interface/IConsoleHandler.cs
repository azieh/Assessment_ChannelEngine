using System;

namespace Assessment_ChannelEngine.Console.Interface
{
    public interface IConsoleHandler
    {
        public void Start();
        public void GetCancelEventHandler(object sender, ConsoleCancelEventArgs args);
        public void WriteAvailableCommands();
    }
}