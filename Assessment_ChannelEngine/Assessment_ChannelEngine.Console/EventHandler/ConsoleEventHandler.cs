using System;

namespace Assessment_ChannelEngine.Console.EventHandler
{
    public class ConsoleEventHandler
    {
        public ConsoleEventHandler(object sender, ConsoleCancelEventArgs args)
        {
            System.Console.WriteLine("\nThe read operation has been interrupted.");

            System.Console.WriteLine($"  Key pressed: {args.SpecialKey}");

            System.Console.WriteLine($"  Cancel property: {args.Cancel}");

            // Set the Cancel property to true to prevent the process from terminating.
            System.Console.WriteLine("Setting the Cancel property to true...");
            args.Cancel = true;

            // Announce the new value of the Cancel property.
            System.Console.WriteLine($"  Cancel property: {args.Cancel}");
            System.Console.WriteLine("The read operation will resume...\n");
        }


    }
}