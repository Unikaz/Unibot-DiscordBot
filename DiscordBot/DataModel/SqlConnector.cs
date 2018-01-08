using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Web.Security;

namespace DiscordBot.DataModel
{
    public class SqlConnector
    {
        private MySqlConnection mySqlConnection;
        private Queue<string> _queries;
        private static SqlConnector _instance = null;

        private SqlConnector()
        {
            mySqlConnection = new MySqlConnection(BotConfig.get().ConnectionString());
        }

        public static SqlConnector Get => _instance ?? (_instance = new SqlConnector());

        public async Task<MySqlDataReader> QueryAsync(string query)
        {
            MySqlDataReader mySqlDataReader;
            
            using (mySqlConnection)
            {
                mySqlConnection.Open();
                mySqlDataReader = await new MySqlCommand(query, mySqlConnection).ExecuteReaderAsync();
                mySqlConnection.Close();
            }
            return mySqlDataReader;       
        }
    }
}