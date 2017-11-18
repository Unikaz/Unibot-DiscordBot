using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Modules
{
    public class Role : ModuleBase<SocketCommandContext>
    {
        [Command("role")]
        public async Task RoleAsync(params string[] args)
        {
            if (args.Length == 2)
            {
                SocketRole targetRole = Context.Guild.Roles.First(role => role.Name.Equals(args[1]));
                if (targetRole != null)
                {
                    if (targetRole.Name.EndsWith("_"))
                    {
                        switch (args[0])
                        {
                            case "add":
                            case "+":
                                (Context.User as IGuildUser).AddRoleAsync(targetRole);
                                ReplyAsync("add role ");
                                break;
                            case "remove":
                            case "rm":
                            case "-":
                                (Context.User as IGuildUser).RemoveRoleAsync(targetRole);
                                ReplyAsync("remove role ");
                                break;
                            default:
                                ReplyAsync("You have to choose between add or remove, that's all I got");
                                break;
                        }
                    }
                    else
                    {
                        ReplyAsync("You can't change this role");
                    }
                }
                else
                {
                    ReplyAsync("The specified role doesn't exist");
                }
            }
            else if (args.Length == 1 && args[0] == "list")
            {
                var list = Context.Guild.Roles.Where(role => role.Name.EndsWith("_")).Select(role => role.Name).ToArray();
                ReplyAsync("Liste des rôles cosmétiques : \n```\n" + string.Join("\n", list) + "```");
            }
            else
            {
                ReplyAsync("This command needs parameters : `!role <add|remove> <roleName>`");
            }
        }
    }
}