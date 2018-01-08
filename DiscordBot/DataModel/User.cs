using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using MySql.Data.MySqlClient;


namespace DiscordBot.DataModel
{
    //===================
    // manager
    //===================
    public class UsersManager
    {
        private static UsersManager _instance;

        private UsersManager()
        {
            mySqlConnection = new MySqlConnection(BotConfig.get().ConnectionString());
        }

        public static UsersManager Get => _instance ?? (_instance = new UsersManager());

        //-------------------------------------

        private MySqlConnection mySqlConnection;
        Dictionary<string, User> _users = new Dictionary<string, User>();

        public async Task<User> GetUserAsync(SocketUser socketUser)
        {
            User user = null;
            string uniqueId = socketUser.Username + "#" + socketUser.Discriminator;
            // if the player is already loaded
            if (_users.TryGetValue(uniqueId, out user)) return user;
            // else we try to get it from DB
            try
            {
                await mySqlConnection.OpenAsync();
//                todo prepare statement to avoid sqlInjection !
//                 free the cpu using async/await if the MySQL has a hard time
                MySqlDataReader dataReader =
                    await new MySqlCommand("select * from users where uniqueId='" + uniqueId + "';", mySqlConnection)
                        .ExecuteReaderAsync();
//                var dataReader = await SqlConnector.Get.QueryAsync("select * from users where uniqueId='" + uniqueId + "';");
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    user = new User(
                        dataReader.GetString("uniqueId"),
                        dataReader.GetBoolean("bot"))
                        {Score = dataReader.GetInt32("cookies")};

                    dataReader.Close();
                    mySqlConnection.CloseAsync();
                    _users[uniqueId] = user;
                }
                else
                {
                    dataReader.Close();
                    mySqlConnection.CloseAsync();
                    Console.Out.WriteLine("Can't find client " + uniqueId + " in database, creation...");
                    user = new User(uniqueId, socketUser.IsBot);
                    //async cause i dont need it
                    RegisterInDbAsync(user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return user;
        }

        public User GetUser(string uniqueId)
        {
            User user = null;
            if (!_users.TryGetValue(uniqueId, out user))
            {
                try
                {
                    mySqlConnection.OpenAsync().Wait();
                    MySqlDataReader dataReader =
                        //todo prepare statement to avoid sqlInjection !
                        new MySqlCommand("select * from users where uniqueId='" + uniqueId + "';", mySqlConnection)
                            .ExecuteReader();
//                    var mySqlDataReaderTask = SqlConnector.Get.QueryAsync("select * from users where uniqueId='" + uniqueId + "';");
//                    mySqlDataReaderTask.Wait();
//                    var dataReader = mySqlDataReaderTask.Result;
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        user = new User(
                            dataReader.GetString("uniqueId"),
                            dataReader.GetBoolean("bot"))
                            {Score = dataReader.GetInt32("cookies")};
                        
                        dataReader.Close();
                        mySqlConnection.CloseAsync();
                        _users[uniqueId] = user;
                    }
                    else
                    {
                        dataReader.Close();
                        mySqlConnection.CloseAsync();
                        Console.Out.WriteLine("Can't find client " + uniqueId + " in database, creation...");
                        user = new User(uniqueId, false);
                        RegisterInDbAsync(user);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return user;
        }

        public User GetUser(string name, int discriminator)
        {
            string userId = name + "#" + discriminator;
            return GetUser(userId);
        }

        /**
         *  Try to get a user by its name, so need to call DB everytime... shouldn't use this
         */
        public async Task<User> GetUserByNameAsync(string name)
        {
            User user = null;
            try
            {
                await mySqlConnection.OpenAsync();
                //todo prepare statement to avoid sqlInjection !
                var dataReader = await new MySqlCommand("select * from users where uniqueId like '" + name + "%';",
                        mySqlConnection)
                    .ExecuteReaderAsync();
                
//                var dataReader =
//                    await SqlConnector.Get.QueryAsync("select * from users where uniqueId like '" + name + "%';");
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    user = new User(
                        dataReader.GetString("uniqueId"),
                        dataReader.GetBoolean("bot"))
                        {Score = dataReader.GetInt32("cookies")};
                }
                dataReader.Close();
                mySqlConnection.CloseAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return user;
        }

        public async Task RegisterInDbAsync(User user)
        {
            try
            {
                await mySqlConnection.OpenAsync();
                //todo prepare statement to avoid sqlInjection !
                await new MySqlCommand("insert into users " +
                                       "(uniqueId, bot, cookies) values " +
                                       "('" + user.UniqueId + "'," + user.Bot + ", " + user.Score +") " +
                                       "on duplicate key update " +
                                       "bot=" + user.Bot + ", "+
                                       "cookies=" + user.Score + " "+
                                       ";", mySqlConnection)
                        .ExecuteNonQueryAsync();
//                SqlConnector.Get.QueryAsync("insert into users " +
//                                            "(uniqueId, bot, cookies) values " +
//                                            "('" + user.UniqueId + "'," + user.Bot + ", " + user.Score + ") " +
//                                            "on duplicate key update " +
//                                            "bot=" + user.Bot + ", " +
//                                            "cookies=" + user.Score + " " +
//                                            ";");
                
                mySqlConnection.CloseAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }


    //===================
    // Instance
    //===================
    public class User
    {
        public string UniqueId { get; }
        public string Name { get; }
        public int Discriminator { get; }
        public bool Bot { get; set; }
        public int Score { get; set; }

        public User(string uniqueId, bool bot)
        {
            UniqueId = uniqueId;
            try
            {
                var split = UniqueId.Split('#');
                Name = split[0];
                Discriminator = int.Parse(split[1]);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(
                    "You try to create a User without giving its uniqueId (something like 'name#0000')");
                Console.Out.WriteLine(e);
            }
            Bot = bot;
        }
    }
}