using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.CommandsChallenges
{
    public class Math : ModuleBase<SocketCommandContext>
    {
        [Command("math")]
        public async Task MathAsync(params string[] args)
        {
            string str = args[1];
            ReplyAsync("!rep " + args[0] + " " + process(str));
        }

        public int process(string str)
        {
            string[] ope;
            ope = str.Split('+');
            if (ope.Length > 0)
            {
                return int.Parse(ope[0]) + int.Parse(ope[1]);
            }
            ope = str.Split('-');
            if (ope.Length > 0)
            {
                return int.Parse(ope[0]) - int.Parse(ope[1]);
            }
            ope = str.Split('*');
            if (ope.Length > 0)
            {
                return int.Parse(ope[0]) * int.Parse(ope[1]);
            }
            ope = str.Split('/');
            if (ope.Length > 0)
            {
                return int.Parse(ope[0]) / int.Parse(ope[1]);
            }
            return 0;
        }
    }
}