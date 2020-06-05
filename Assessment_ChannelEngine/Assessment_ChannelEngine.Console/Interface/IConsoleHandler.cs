using System;

namespace Assessment_ChannelEngine.Console.Interface
{
    public interface IConsoleHandler
    {
        /// <summary>Starts this instance.</summary>
        public void Start();
        /// <summary>Gets the cancel event handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ConsoleCancelEventArgs"/> instance containing the event data.</param>
        public void GetCancelEventHandler(object sender, ConsoleCancelEventArgs args);
        /// <summary>Writes the available commands.</summary>
        public void WriteAvailableCommands();
    }
}