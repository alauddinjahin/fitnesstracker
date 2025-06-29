using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Config
{
    class FtDB
    {
        private static readonly string databaseFileName = "FT_DB.mdf";

        private static string GetConnectionString()
        {

            return $@"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=|DataDirectory|\{databaseFileName};
                    Integrated Security=True;";

        }

        public static SqlConnection GetDbConnection()
        {
            string connectionString = GetConnectionString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
