using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace RPS_Man
{
    class Program
    {
        public const String botname = "the RPS-Man"; //your bot's name here

        static void Main(string[] args)
        {
            Console.WriteLine($"Started “{botname}“ successfully!");
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "Your neat Token here!", //put your token in here ofc
                TokenType = TokenType.Bot
            });

            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<SlashCommands>(/*Input your Guild's ID in here to immediately test the commands; leave it empty to register them globally (takes up to a hour to register them globally!)*/);

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        public class SlashCommands : SlashCommandModule
        {
            [SlashCommand("ping", "Checks the ping of " + botname)]
            public async Task pingCmd(InteractionContext ctx)
            {
                var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{emoji} Pong!\nIt took {botname} {ctx.Client.Ping}ms to respond!"));
            }

            [SlashCommand("Rock-Paper-Scissors", "Play Rock Paper Scissors with " + botname)]
            public async Task rpsCmd(InteractionContext ctx, [Choice("Rock", "Rock")][Choice("Paper", "Paper")][Choice("Scissors", "Scissors")][Option("Your-Weapon", "Choose the weapon you want to fight with")] string chose)
            {
                Random rand = new Random();
                string[] toChooseFrom =
                {
                  "Rock",
                  "Paper",
                  "Scissors"
                };

                string botChose = null;
                string whoWins = null;

                botChose = toChooseFrom[rand.Next(toChooseFrom.Length)];

                switch (chose)
                {
                    case "Rock":
                        switch (botChose)
                        {
                            case "Rock":
                                whoWins = "Draw!";
                                break;
                            case "Paper":
                                whoWins = $"{botname} wins!";
                                break;
                            case "Scissors":
                                whoWins = "You win!";
                                break;
                        }
                        break;
                    case "Paper":
                        switch (botChose)
                        {
                            case "Rock":
                                whoWins = "You win!";
                                break;
                            case "Paper":
                                whoWins = "Draw!";
                                break;
                            case "Scissors":
                                whoWins = $"{botname} wins!";
                                break;
                        }
                        break;
                    case "Scissors":
                        switch (botChose)
                        {
                            case "Rock":
                                whoWins = $"{botname} wins!";
                                break;
                            case "Paper":
                                whoWins = "You win!";
                                break;
                            case "Scissors":
                                whoWins = "Draw!";
                                break;
                        }
                        break;
                }

                string response = new string($"You chose {chose}\n{botname} chose {botChose}\n{whoWins}");
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(response));
            }
        } 
    }
}
