using System;
using System.Threading.Tasks;
using DSharpPlus;
using swolecore.messages;

namespace discordclient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync() {
            string discordToken = Environment.GetEnvironmentVariable("SWOLEBOT_DISCORDTOKEN");
            var discord = new DiscordClient(new DiscordConfiguration() {
                Token = discordToken,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (e) => {
                if (e.Message.Content.ToLower().StartsWith("!ping")) {
                    await e.Message.RespondAsync("pong!");
                } else if (e.Message.Content.ToLower().StartsWith("!register")) {
                    Console.WriteLine("Received register command!");
                    try {
                        User user = new User() {
                            DiscordId = e.Author.Id
                        };

                        // Send message to register the user
                        Console.WriteLine($"Sending user message... with email = {user.Email}; id = {user.DiscordId}");
                    } catch (Exception ex) {
                        Console.WriteLine($"An exception occurred.. {ex.Message}");
                    }
                    
                    await e.Message.RespondAsync($"Registering user {e.Author.Username}");
                    Console.WriteLine("Sent response!");
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
