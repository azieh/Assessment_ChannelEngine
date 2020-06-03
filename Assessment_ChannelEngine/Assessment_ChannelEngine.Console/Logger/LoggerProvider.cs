using Microsoft.Extensions.Logging;

namespace Assessment_ChannelEngine.Console.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger();
        }

        public void Dispose()
        {
        }
    }
}
