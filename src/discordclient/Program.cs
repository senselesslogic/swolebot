using System;
using System.Threading.Tasks;
using swolecore.messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;

namespace discordclient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = setupDI();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogDebug("Starting Application.");

            var discordWrapper = serviceProvider.GetService<IDiscordWrapper>();
            await discordWrapper.Start();

            await Task.Delay(-1);
        }

        private static ServiceProvider setupDI() {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging((builder) => {
                builder.AddConsole();
                builder.AddFilter("discordclient", LogLevel.Debug);
            });

            serviceCollection.AddSingleton<IDiscordWrapper, DiscordWrapper>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
