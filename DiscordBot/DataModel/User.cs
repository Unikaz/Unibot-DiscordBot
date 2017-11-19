using System;
using System.Collections.Generic;
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

        public User GetUser(string uniqueName)
        {
            var split = uniqueName.Split('#');
            return GetUser(split[0], int.Parse(split[1]));
        }

        public User GetUser(string name, int discriminator)
        {
            User user = null;
            string userId = name + "#" + discriminator;
            if (!_users.TryGetValue(userId, out user))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlDataReader dataReader =
                        //todo prepare statement to avoid sqlInjection !
                        new MySqlCommand(
                                "select * from users where name='" + name + "' and discriminator=" + discriminator +
                                ";", mySqlConnection)
                            .ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        user = new User(
                            dataReader.GetString("name"),
                            dataReader.GetInt16("discriminator"),
                            dataReader.GetBoolean("bot"));

                        _users[userId] = user;
                    }
                    mySqlConnection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return user;
        }

        /**
         *  Try to get a user by its name, so need to call DB everytime... shouldn't use this
         */
        public User GetUserByName(string name)
        {
            User user = null;
            try
            {
                mySqlConnection.Open();
                //todo prepare statement to avoid sqlInjection !
                MySqlDataReader dataReader =
                    new MySqlCommand("select * from users where name='" + name + "';", mySqlConnection).ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    user = new User(
                        dataReader.GetString("name"),
                        dataReader.GetInt16("discriminator"),
                        dataReader.GetBoolean("bot"));
                }
                mySqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return user;
        }

        public bool Register(User user)
        {
            try
            {
                mySqlConnection.Open();
                //todo prepare statement to avoid sqlInjection !
                MySqlCommand command =
                    new MySqlCommand("insert into users (name, discriminator, bot) values ('" + user.Name() + "'," +
                                     user.Discriminator() + "," + user.Bot() + ");");
                command.Connection = mySqlConnection;
                Console.Out.WriteLine(command.ExecuteNonQuery());
                Console.Out.WriteLine("done ?");

                mySqlConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                mySqlConnection.Close();
                Console.WriteLine(e);
                return false;
            }
        }
    }


    //===================
    // Instance
    //===================
    public class User
    {
        private string _name;
        private int _discriminator;
        private bool _bot;
        private string _test;

        public User(string name, int discriminator, bool bot)
        {
            _name = name;
            _discriminator = discriminator;
            _bot = bot;
        }


        public string Name() => _name;
        public int Discriminator() => _discriminator;
        public bool Bot() => _bot;
    }
}