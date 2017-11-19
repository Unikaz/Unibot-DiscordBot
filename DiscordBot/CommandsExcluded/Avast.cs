using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.CommandsExcluded
{
    public class Avast : ModuleBase<SocketCommandContext>
    {
        [Command("avast")]
        public async Task AvastAsync(params string[] args)
        {
            //to dangerous ^^
            //await ReplyAsync("la base virale a été mise à jour", true);
        }
    }
}