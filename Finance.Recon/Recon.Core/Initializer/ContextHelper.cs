using System.Collections.Generic;

namespace Recon.Core.Initializer
{
    public class ContextHelper
    {
        public Dictionary<int, IList<string>> Migrations { get; set; }

        public ContextHelper()
        {
            Migrations = new Dictionary<int, IList<string>>();

            Migrations.Add(1, MigrationVersion1());
            Migrations.Add(2, MigrationVersion2());
        }

        private IList<string> MigrationVersion1()
        {
            IList<string> steps = new List<string>();

            steps.Add("CREATE TABLE \"ReconFrom\" ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Reference] nvarchar, [Amount] decimal NOT NULL);");
            steps.Add("CREATE TABLE \"ReconTo\" ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Reference] nvarchar, [Amount] decimal NOT NULL);");
            steps.Add("CREATE TABLE \"SchemaInfo\" ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Version] int NOT NULL);");

            return steps;
        }

        private IList<string> MigrationVersion2()
        {
            IList<string> steps = new List<string>();

            steps.Add("create view MatchToView as select a.Reference, a.Amount, b.Reference, b.Amount, CASE WHEN a.Amount = b.Amount THEN 'Y' ELSE 'N' END[Match], CASE WHEN a.Amount <> b.Amount THEN(a.Amount - b.Amount) ELSE 0 END[Difference] from ReconTo a left outer join ReconFrom b on a.Reference = b.Reference");
            steps.Add("create view MatchFromView as select a.Reference, a.Amount, b.Reference, b.Amount, CASE WHEN a.Amount = b.Amount THEN 'Y' ELSE 'N' END[Match], CASE WHEN a.Amount <> b.Amount THEN(a.Amount - b.Amount) ELSE 0 END[Difference] from ReconFrom a left outer join ReconTo b on a.Reference = b.Reference");

            return steps;
        }

    }

}
