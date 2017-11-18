using System;
using System.Configuration;

namespace DiscordBot
{
    public class BotConfig
    {
        private String _token;

        public BotConfig()
        {
            _token = ConfigurationManager.AppSettings["token"];
        }

        public string GetToken()
        {
            return _token;
        }
    }
}