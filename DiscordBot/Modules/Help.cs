using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Alias("aide")]
        public async Task HelpAsync(params string[] args)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.Title = "Unibot's commands";
            embedBuilder.AddField("!hello", "- dit bonjour");
            embedBuilder.AddField("!help", "- affiche ce message");
            embedBuilder.AddField("!role <add|remove> <roleName>", "- ajoute ou enlève le rôle cosmétique précisé");
            embedBuilder.AddField("!whois \"<Pseudo>\"", "- pour en apprendre plus sur quelqu'un ^^");
            embedBuilder.AddField("!vote \"<description>\" <option1, option2, option3, ...>", "- propose un vote");
            var embed = embedBuilder.Build();
            await ReplyAsync("", false, embed);
        }
    }
}