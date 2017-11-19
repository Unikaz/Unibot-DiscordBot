using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.CommandsExcluded
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task TestAsync(params string[] args)
        {
        }
    }
}