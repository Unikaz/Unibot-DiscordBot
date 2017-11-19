using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Commands
{
    public class Whois : ModuleBase<SocketCommandContext>
    {
        [Command("whois")]
        public async Task WhoisAsync(params string[] args)
        {
            if (args.Length == 1)
            {
                SocketGuildUser[] targetArray = Context.Guild.Users.Where(user => user.Username == args[0]).ToArray();
                if (targetArray.Length == 0)
                {
                    targetArray = Context.Guild.Users.Where(user => user.Mention.ToString() == args[0]).ToArray();
                    if (targetArray.Length == 0)
                    {
                        ReplyAsync("Il semblerait que cette personne n'existe pas (ou alors c'est parce que la mention ne marche pas à tout les coups...)");
                        return;
                    }
                }
                SocketGuildUser target = targetArray[0];
                ReplyAsync(target.Mention + ", " + Context.User.Mention + " voudrait apprendre à te connaitre :heart:");
            }
            else
            {
                ReplyAsync("Il faut préciser qui tu cherches");
            }
        }
    }
}