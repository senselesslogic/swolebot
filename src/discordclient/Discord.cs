using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DSharpPlus;
using swolecore.messages;
using System;

namespace discordclient {
    public interface IDiscordWrapper {
        Task Start();
    }

    public class DiscordWrapper : IDiscordWrapper {
        private readonly ILogger<DiscordWrapper> logger;
        private readonly DiscordClient client;

        public DiscordWrapper(ILoggerFactory loggerFactory) {
            logger = loggerFactory.CreateLogger<DiscordWrapper>();

            string discordToken = Environment.GetEnvironmentVariable("SWOLEBOT_DISCORDTOKEN");
            logger.LogDebug($"{discordToken}");
            client = new DiscordClient(new DiscordConfiguration() {
                Token = discordToken,
                TokenType = TokenType.Bot
            });

            SetupClient();
        }

        private void SetupClient() {
            client.MessageCreated += async (e) => {
                if (e.Message.Content.ToLower().StartsWith("!ping")) {
                    await e.Message.RespondAsync("pong!");
                } else if (e.Message.Content.ToLower().StartsWith("!register")) {
                    logger.LogDebug("Received register command!");

                    try {
                        User user = new User() {
                            DiscordId = e.Author.Id
                        };

                        // Send message to register the user
                        logger.LogDebug($"Sending user message... id = {user.DiscordId}");
                    } catch (Exception ex) {
                        logger.LogError($"An exception occurred.. {ex.Message}");
                    }

                    await e.Message.RespondAsync($"Registering user {e.Author.Username}");
                    logger.LogDebug("Sent response!");
                }
            };
        }

        public async Task Start() {
            await client.ConnectAsync();
            logger.LogDebug("Connection established.");
        }
    }
}