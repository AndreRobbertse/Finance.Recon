using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Recon.Core.Contexts;
using Recon.Interface;

namespace Recon.Core
{
    public class Setup
    {
        public Setup(IEnumerable<IRecon> fromRecons, IEnumerable<IRecon> toRecons)
        {
            //Console.WriteLine("Check DB: {0}", Util.DbPath);
            //if (!File.Exists(Util.DbPath))
            //{
            //    createDatabase();
            //}

            (new ReconContext()).Initialize(fromRecons.ToList(), toRecons.ToList());
        }

        private void createDatabase()
        {
            Console.WriteLine("Creating DB: {0}", Lookup.DatabaseName);
            SQLiteConnection.CreateFile(Lookup.DatabaseName);
        }
        }
}