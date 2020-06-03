using System;
using Assessment_ChannelEngine.Console.Interface;
using Microsoft.Extensions.Logging;

namespace Assessment_ChannelEngine.Console
{
    public class ConsoleHandler : IConsoleHandler
    {
        private readonly ILogger<ConsoleHandler> _logger;
        private readonly IOrdersServiceProxy _ordersServiceProxy;
        private ConsoleKeyInfo _cki;
        public ConsoleHandler(ILogger<ConsoleHandler> logger, IOrdersServiceProxy ordersServiceProxy)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ordersServiceProxy = ordersServiceProxy ?? throw new ArgumentNullException(nameof(ordersServiceProxy));

            System.Console.Clear();
            // Establish an event handler to process key press events.
            System.Console.CancelKeyPress += GetCancelEventHandler;
        }
        /// <inheritdoc />
        public void Start()
        {
            WriteAvailableCommands();
            while (true)
            {
                var command = System.Console.ReadLine();
                switch (command)
                {
                    case nameof(_ordersServiceProxy.FetchAllIN_PROGRESS):
                        _ordersServiceProxy.FetchAllIN_PROGRESS();
                        break;

                    case nameof(_ordersServiceProxy.GetTop5SoldProductByQuantity):
                        _ordersServiceProxy.GetTop5SoldProductByQuantity();
                        break;

                    case nameof(_ordersServiceProxy.SetQuantityTo25):
                        _ordersServiceProxy.SetQuantityTo25();
                        break;
                    default:
                        _logger.LogWarning("Unknown Command " + command);
                        break;
                }
            }
        }

        /// <inheritdoc />
        public void GetCancelEventHandler(object sender, ConsoleCancelEventArgs args)
        {
            _logger.LogInformation("\nThe close operation key has been pressed");

            _logger.LogInformation($"  Key pressed: {args.SpecialKey}");

            System.Environment.Exit(0);
        }

        /// <inheritdoc />
        public void WriteAvailableCommands()
        {
            _logger.LogInformation("List of available commands");
            _logger.LogInformation($"{nameof(_ordersServiceProxy.FetchAllIN_PROGRESS)}");
            _logger.LogInformation($"{nameof(_ordersServiceProxy.GetTop5SoldProductByQuantity)}");
            _logger.LogInformation($"{nameof(_ordersServiceProxy.SetQuantityTo25)}");
        }
    }
}