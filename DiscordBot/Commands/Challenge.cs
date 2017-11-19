using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Challenges;
using Microsoft.VisualBasic;

namespace DiscordBot.Commands
{
    public class Challenge : ModuleBase<SocketCommandContext>
    {
        public static string roleNAme = "bots"; 
        public static SocketRole _role = null;

        [Command("challenge")]
        public async Task ChallengeAsync(params string[] args)
        {
            if (args.Length == 0)
            {
                if (_role == null)
                {
                    _role = Context.Guild.Roles.Where(role => role.Name == roleNAme).ToList()[0];
                }
                //-------------- TEMP ------------------
                //random math
                Random random = new Random();
                int a = random.Next(0, 1000);
                int b = random.Next(0, 1000);
                int res = a + b;
                string type = "math";
                Challenges.Challenge challenge =
                    ChallengesManager.Get.CreateChallenge(type, a + "+" + b, res + "", DateAndTime.Now);
                //-------------- TEMP ------------------
                ReplyAsync(_role.Mention + " " + type + " #" + challenge.GetID() + " " + challenge.GetQuestion());
            }
            else if(args.Length == 1)
            {
                int id;
                if(int.TryParse(args[0], out id))
                    ReplyAsync("", false, ChallengesManager.Get.GetChallenge(id).GetResultsEmbed());
            }
        }
    }
}