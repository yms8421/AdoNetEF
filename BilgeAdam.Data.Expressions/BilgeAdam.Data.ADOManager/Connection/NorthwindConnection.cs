using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BilgeAdam.Data.ADOManager.Connection
{
    public static class NorthwindConnection
    {
        private static readonly string connectionString;
        static NorthwindConnection()
        {
            //ilk çağrıda bir defa çalışır ve bir daha çalışmaz!!! non-static constructor'dan farklıdır
            connectionString = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        }
        public static SqlConnection Connection
        {
            get
            {
                return GetConnection();
            }
        }
        private static SqlConnection connection;
        private static object lockObject = new object();
        private static SqlConnection GetConnection()
        {
            //SINGLETON DESIGN PATTERN
            if (connection == null)
            {
                lock (lockObject)
                {
                    if (connection == null)
                    {
                        connection = new SqlConnection(connectionString);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }
                        
                    }
                }
            }
            return connection;
        }
    }


}
