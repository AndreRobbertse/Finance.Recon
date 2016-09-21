using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Recon.Core
{
    internal class Util
    {
        public static readonly string DbPath = Path.Combine(Application.StartupPath, Lookup.DatabaseName);
        public static readonly string RelativePath = @"database\" + Lookup.DatabaseName;
        public static readonly string CurrentPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static readonly string AbsolutePath = Path.Combine(CurrentPath, RelativePath);

        private static string _connectionString = string.Empty;
        public static string SqliteConnectionString
        {
            get
            {
                if (_connectionString == string.Empty)
                {
                    var csb = new SQLiteConnectionStringBuilder { DataSource = Util.DbPath };
                    var dbconnection = new SQLiteConnection(csb.ConnectionString + ";Integrated Security=True;Persist Security Info=True;Initial Catalog=Recons");
                    
                    // create a new database connection:
                    //sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
                    _connectionString = dbconnection.ConnectionString;
                }
                return _connectionString;
            }
        }

        private string BuildConnection()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = DbPath, // server address
                //InitialCatalog = databaseName, // database name
                //IntegratedSecurity = false, // server auth(false)/win auth(true)
                //MultipleActiveResultSets = false, // activate/deactivate MARS
                //PersistSecurityInfo = true, // hide login credentials
                //UserID = userName, // user name
                //Password = password // password
            };
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }

        public static void CreateDatabase()
        {
            SQLiteConnection.CreateFile(Util.DbPath);
        }
    }
}
