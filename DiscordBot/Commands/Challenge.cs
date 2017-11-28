using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Challenge;

namespace DiscordBot.Commands
{
    public class Challenge : ModuleBase<SocketCommandContext>
    {
        public static string roleName = "bots";
        public static SocketRole _role = null;

        [Command("challenge")]
        public async Task ChallengeAsync(params string[] args)
        {
            if (_role == null)
            {
                _role = Context.Guild.Roles.Where(role => role.Name == roleName).ToList()[0];
            }
            if (args.Length == 0)
            {
                // create a random challenge
                AChallenge challenge = ChallengesManager.Get.CreateChallenge();
                EmbedBuilder embedBuilder = challenge.GetQuestionEmbedBuilder();
                if (embedBuilder != null)
                {
                    ReplyAsync(
                        _role.Mention + " " + challenge.GetName() + " #" + challenge.GetId() + " " +
                        challenge.GetQuestion(), false, challenge.GetQuestionEmbedBuilder());
                }
                else
                {
                    ReplyAsync(
                        _role.Mention + " " + challenge.GetName() + " #" + challenge.GetId() + " " +
                        challenge.GetQuestion());
                }
            }
            else if (args.Length == 1)
            {
                int id;
                if (int.TryParse(args[0], out id))
                {
                    // display result for the specified challenge
                    ReplyAsync("", false, ChallengesManager.Get.GetChallenge(id).GetResultsEmbed());
                }
                else
                {
                    // create the specified challenge
                    AChallenge challenge = ChallengesManager.Get.CreateChallenge(args[0]);
                    ReplyAsync(_role.Mention + " " + challenge.GetName() + " #" + challenge.GetId() + " " +
                               challenge.GetQuestion(), false, challenge.GetQuestionEmbedBuilder());
                }
            }
        }
    }
}