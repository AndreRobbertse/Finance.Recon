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

            steps.Add("CREATE TABLE \"ReconFrom\" ([Id] integer, [Reference] nvarchar, [Amount] decimal NOT NULL, PRIMARY KEY(Id));");
            steps.Add("CREATE TABLE \"ReconTo\" ([Id] integer, [Reference] nvarchar, [Amount] decimal NOT NULL, PRIMARY KEY(Id));");
            steps.Add("CREATE TABLE \"SchemaInfo\" ([Id] integer, [Version] int NOT NULL, PRIMARY KEY(Id));");

            Migrations.Add(1, steps);
        }

    }

}
