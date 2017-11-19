using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.DataModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;

namespace DiscordBot
{
    internal class Program
    {
        // launcher
        public static void Main(string[] args) => new Program().start().GetAwaiter().GetResult();


        private DiscordSocketClient _client;
        private CommandService _command;
        private IServiceProvider _services;
        private BotConfig _config;


        public async Task start()
        {
            _config = BotConfig.get(); // get data from config
            _client = new DiscordSocketClient();
            _command = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_command)
                .BuildServiceProvider();

            //event subscriptions
            _client.Log += Log;

            // register commands
            await RegisterCommandsAsync();

            // start connection
            await _client.LoginAsync(TokenType.Bot, BotConfig.get().Token);
            await _client.StartAsync();

            // wait indefinitely
            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.Delay(0); // hack cause I don't how to move to .Net 4.6 to use CompletedTask
        }

        //========================================
        // Commands
        //========================================

        public async Task RegisterCommandsAsync()
        {
            // Add a handler on MessageReceived event
            _client.MessageReceived += HandleCommandAsync;
            // seems to add content of the Modules folder using reflection
            await _command.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null)
                return;


            bool botsTag = arg.MentionedRoles.Any(Role => Role.Name == "bots");
            SocketRole socketRole = null;
            if (botsTag)
                socketRole = arg.MentionedRoles.First(Role => Role.Name == "bots");


            // dont know what is this used for...
            int argPos = 0;
            // check if we have to handle the message
            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos) || botsTag)
            {
                if (botsTag)
                    argPos = socketRole.Mention.Length+1;

                var context = new SocketCommandContext(_client, message);
                var result = await _command.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}