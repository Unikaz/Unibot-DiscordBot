using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.Commands
{
    public class Hello : ModuleBase<SocketCommandContext>
    {
        [Command("hello")]
        [Alias("yop", "hi")]
        public async Task HelloAsync(params string[] args)
        {
            await ReplyAsync("Hello "+ Context.User.Mention +" !");
        }
    }
}