using System;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBot.DataModel;

namespace DiscordBot.CommandsExcluded
{
    public class Register : ModuleBase<SocketCommandContext>
    {
        [Command("register")]
        public async Task RegisterAsync(params string[] args)
        {
            User user = new User(Context.User.Username + "#" + Context.User.DiscriminatorValue, Context.User.IsBot);
            UsersManager.Get.RegisterInDbAsync(user);
            Console.Out.WriteLine("register " + user.Name);
        }
    }
}