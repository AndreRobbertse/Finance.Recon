using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using Recon.Core.Contexts;
using Recon.Interface;

namespace Recon.Core
{
    public class Setup
    {
        public Setup(IEnumerable<IRecon> fromRecons, IEnumerable<IRecon> toRecons)
        {
            if (File.Exists(Util.DbPath))
            {
                File.Delete(Util.DbPath);
            }
            
            (new ReconContext()).Initialize(fromRecons.ToList(), toRecons.ToList());
        }

        private void createDatabase()
        {
            Console.WriteLine("Creating DB: {0}", Lookup.DatabaseName);
            SQLiteConnection.CreateFile(Lookup.DatabaseName);
        }
    }
}