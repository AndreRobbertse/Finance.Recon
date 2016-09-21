using System.Collections.Generic;

namespace Recon.Core.Initializer
{
    public class ContextHelper
    {
        public ContextHelper()
        {
            Migrations = new Dictionary<int, IList<string>>();

            MigrationVersion1();
        }

        public Dictionary<int, IList<string>> Migrations { get; set; }

        private void MigrationVersion1()
        {
            IList<string> steps = new List<string>();

            steps.Add("CREATE TABLE \"SchemaInfo\" (\"Id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL, \"Version\" INTEGER)");
            steps.Add("CREATE TABLE \"ReconFrom\" (\"Id\" TEXT, \"Amount\" TEXT)");
            steps.Add("CREATE TABLE \"ReconTo\" (\"Id\" TEXT, \"Amount\" TEXT)");

            Migrations.Add(1, steps);
        }
    }

}
