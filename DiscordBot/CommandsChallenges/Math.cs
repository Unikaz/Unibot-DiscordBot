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
            ReplyAsync("!rep " + args[0] + " " + (process(str)+"").Replace(",", "."));
        }

        public float process(string str)
        {
            string[] ope;
            if (str.Contains("+"))
            {
                ope = str.Split('+');
                return int.Parse(ope[0]) + int.Parse(ope[1]);
            }
            if (str.Contains("-"))
            {
                ope = str.Split('-');
                return int.Parse(ope[0]) - int.Parse(ope[1]);
            }
            if (str.Contains("*"))
            {
                ope = str.Split('*');
                return int.Parse(ope[0]) * int.Parse(ope[1]);
            }
            if (str.Contains("/"))
            {
                ope = str.Split('/');
                return ((int)(int.Parse(ope[0]) / float.Parse(ope[1])*100))/100f;
            }
            return 0;
        }
    }
}