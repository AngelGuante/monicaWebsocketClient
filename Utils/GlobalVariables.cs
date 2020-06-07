using System.Data.SqlClient;
using System.Linq;

namespace monicaWebsocketClient.Utils
{
    public abstract class GlobalVariables
    {
        private static string _connectionString = "Data Source=MIKI\\MONICA10; Initial Catalog=monica10; Integrated Security=true;";
        private static SqlConnection _conn = null;

        public static SqlConnection Conn
        {
            get
            {
                if (_conn == default)
                    _conn = new SqlConnection(_connectionString);

                return _conn;
            }
        }

        public static string[] DbNames { get; } = { "monica10" };
    }
}