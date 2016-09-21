using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using Recon.Core.Contexts;
using Recon.Core.Factory;
using Recon.Core.Initializer;
using Recon.Interface;
using Recon.Model;

namespace Recon.Core
{
    public class Setup
    {
        public Setup()
        {
            //Console.WriteLine("Check DB: {0}", Util.DbPath);
            //if (!File.Exists(Util.DbPath))
            //{
            //    createDatabase();
            //}

            (new ReconContext()).Initialize();

            //var context = new ReconContextInitializer();

            //if (Debugger.IsAttached)
            //{
            //    isValidSqliteConnectionString();
            //}

            //Database.DefaultConnectionFactory = new SqLiteConnectionFactory(Util.DbPath, Util.SqliteConnectionString);
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
                dbconnection.Close();
                dbconnection.Dispose();

                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void InitData(IEnumerable<IRecon> fromRecons, IEnumerable<IRecon> toRecons)
        {
            using (ReconContext context = new ReconContext())
            {
                foreach (var fromR in fromRecons)
                {
                    context.ReconFroms.Add((ReconFrom)fromR);
                }

                foreach (var toR in toRecons)
                {
                    context.ReconTos.Add((ReconTo)toR);
                }

                context.SaveChanges();
            }
        }
    }
}