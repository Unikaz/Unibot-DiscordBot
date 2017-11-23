﻿using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBot.Challenges;

namespace DiscordBot.Commands
{
    public class Rep : ModuleBase<SocketCommandContext>
    {
        [Command("rep")]
        public async Task RepAsync(params string[] args)
        {
            if (args != null && args.Length > 1)
            {
                if (args[0][0] == '#')
                {
                    int challengeId;
                    if (int.TryParse(args[0].Substring(1), out challengeId))
                    {
                        if (challengeId != -1) //todo test challengeId validity
                        {
                            string response = string.Join(" ", args.Skip(1));
                            Challenges.AChallenge aChallenge = ChallengesManager.Get.GetChallenge(challengeId);
                            int duration = aChallenge.GetDuration();
                            bool win = aChallenge.IsCorrect(Context.User, response);
                            ReplyAsync(Context.User.Mention + " a répondu au défi #" + challengeId + " en " + duration + "ms et a eu " + (win ? "juste !" : "faux... essaye encore !"));
                            return;
                        }
                        else
                        {
                            ReplyAsync("le challenge #" + args[0] + " n'existe pas");
                            return;
                        }
                    }
                }
            }
            ReplyAsync("Vous devez répondre sous la forme `!rep #ID réponses`");
        }
    }
}