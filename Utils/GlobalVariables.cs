using System.Data.SqlClient;

namespace monicaWebsocketClient.Utils
{
    public abstract class GlobalVariables
    {
        private static SqlConnection _conn = null;

        public static SqlConnection Conn
        {
            get
            {
                if (_conn == default){
                    var _connectionString = System.IO.File.ReadAllText(@"./ConnectionString.txt");
                    _conn = new SqlConnection(_connectionString);
                }

                return _conn;
            }
        }

        public static string[] DbNames { get; } = { "monica10" };
    }
}