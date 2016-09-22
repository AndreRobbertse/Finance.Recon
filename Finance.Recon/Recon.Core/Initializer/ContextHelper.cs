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

            steps.Add("CREATE TABLE \"ReconFrom\" ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Reference] nvarchar, [Amount] decimal NOT NULL);");
            steps.Add("CREATE TABLE \"ReconTo\" ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Reference] nvarchar, [Amount] decimal NOT NULL);");
            steps.Add("CREATE TABLE \"SchemaInfo\" ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Version] int NOT NULL);");

            Migrations.Add(1, steps);
        }

    }

}
