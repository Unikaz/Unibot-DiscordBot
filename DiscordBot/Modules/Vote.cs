using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Modules
{
    public class Vote : ModuleBase<SocketCommandContext>
    {
        [Command("vote")]
        public async Task VoteAsync(params string[] args)
        {
//            await ReplyAsync("Not implemented Yet!");
            //todo see how to deferentiate a boolean vote and a multi choices vote
            //deferentiate vote type
            // if vote bool -> display with reaction +1 -1
            // else -> enum choice using 1, 2, 3, ...

        }
    }
}