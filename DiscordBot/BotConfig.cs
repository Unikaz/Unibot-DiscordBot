using System;
using System.Configuration;

namespace DiscordBot
{
    public class BotConfig
    {
        private static BotConfig _instance = null;

        private BotConfig()
        {
            _token = ConfigurationManager.AppSettings["token"];
            _dbName = ConfigurationManager.AppSettings["dbName"];
            _dbAdress = ConfigurationManager.AppSettings["dbAdress"];
            _dbPort = ConfigurationManager.AppSettings["dbPort"];
            _dbUsername = ConfigurationManager.AppSettings["dbUsername"];
            _dbPassword = ConfigurationManager.AppSettings["dbPassword"];
        }

        public static BotConfig get()
        {
            if (_instance == null)
                _instance = new BotConfig();
            return _instance;
        }

        //============================
        //============================

        private string _token;
        private string _dbName;
        private string _dbAdress;
        private string _dbPort;
        private string _dbUsername;
        private string _dbPassword;

        public string Token => _token;

        public string DbName => _dbName;

        public string DbAdress => _dbAdress;

        public string DbPort => _dbPort;

        public string DbUsername => _dbUsername;

        public string DbPassword => _dbPassword;

        public string ConnectionString()
        {
            return "database=" + _dbName + ";server=" + _dbAdress + ";user=" + _dbUsername + ";password=" +
                   _dbPassword;
        }
    }
}