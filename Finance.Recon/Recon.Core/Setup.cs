using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace Recon.Core
{
    public class Setup
    {
        public Setup()
        {
            Console.WriteLine("Check DB: {0}", Util.DbPath);
            if (!File.Exists(Util.DbPath))
            {
                createDatabase();
            }
            isValidSqliteConnectionString();
        }

        private void createDatabase()
        {
            Console.WriteLine("Creating DB: {0}", Lookup.DatabaseName);
            SQLiteConnection.CreateFile(Lookup.DatabaseName);
        }

        //private void testConnectionString(string provider)
        //{
        //    Console.WriteLine(Util.SqliteConnectionString);
        //    try
        //    {
        //        DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
        //        using (DbConnection conn = factory.CreateConnection())
        //        {
        //            conn.ConnectionString = csb.ConnectionString;
        //            conn.Open();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        private bool isValidSqliteConnectionString()
        {
            try
            {
                var csb = new SQLiteConnectionStringBuilder { DataSource = Util.DbPath, Version = 3 };
                var dbconnection = new SQLiteConnection(csb.ConnectionString);
                dbconnection.Open();
                //COMMENT: Any command to check whether the database is encrypted
                using (SQLiteCommand command = new SQLiteCommand("PRAGMA schema_version;", dbconnection))
                {
                    var ret = command.ExecuteScalar();
                }
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}