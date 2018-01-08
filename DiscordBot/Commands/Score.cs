using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.DataModel;

namespace DiscordBot.Commands
{
    public class Score : ModuleBase<SocketCommandContext>
    {
        [Command("score")]
        public async Task ScoreAsync(params string[] args)
        {
            if (args.Length == 0)
            {
                var user = await UsersManager.Get.GetUserAsync(Context.User);
                ReplyAsync("Le score de " + user.Name + " est de " + user.Score + (user.Bot ? ":gear:" : ":cookie:"));
            }
            else if (args.Length == 1)
            {
                ulong userId = 0;
                if (MentionUtils.TryParseUser(args[0], out userId))
                {
                    User user = await UsersManager.Get.GetUserAsync(Context.Guild.GetUser(userId));
                    if (user != null)
                        ReplyAsync("Le score de " + user.Name + " est de " + user.Score +
                                   (user.Bot ? ":gear:" : ":cookie:"));
                    else
                    {
                        Console.Out.WriteLine("Can't find the mention ?");
                    }
                }
                else
                {
                    User user = await UsersManager.Get.GetUserByNameAsync(args[0]);
                    if (user != null)
                    {
                        ReplyAsync("Le score de " + user.Name + " est de " + user.Score +
                                   (user.Bot ? ":gear:" : ":cookie:"));
                    }
                    else
                    {
                        ReplyAsync("Impossible de trouver " + args[1] + "... il existe ?");
                    }
                }
            }
        }
    }
}