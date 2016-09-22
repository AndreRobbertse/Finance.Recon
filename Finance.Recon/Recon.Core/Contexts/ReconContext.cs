using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Recon.Core.Initializer;
using Recon.Interface;
using Recon.Model;
using SQLite.CodeFirst;

namespace Recon.Core.Contexts
{
    public class ReconContext : DbContext
    {
        public static int RequiredDatabaseVersion = 1;

        public DbSet<Model.ReconFrom> ReconFroms { get; set; }
        public DbSet<Model.ReconTo> ReconTos { get; set; }
        public DbSet<Model.SchemaInfo> SchemaInfoes { get; set; }

        public ReconContext()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var model = modelBuilder.Build(Database.Connection);
            ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
            var modelGeneratedSQL = sqlGenerator.Generate(model.StoreModel);

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public void Initialize(IList<IRecon> fromRecons, IList<IRecon> toRecons)
        {
            using (ReconContext context = new ReconContext())
            {
                int currentVersion = 0;

                ContextHelper contextHelper = new ContextHelper();
                while (currentVersion < RequiredDatabaseVersion)
                {
                    currentVersion++;
                    foreach (string migration in contextHelper.Migrations[currentVersion])
                    {
                        context.Database.ExecuteSqlCommand(migration);
                    }

                    var versions = context.SchemaInfoes.ToList();
                    if (versions.Count > 0)
                    {
                        currentVersion = context.SchemaInfoes.Max(x => x.Version);
                    }

                    context.SchemaInfoes.Add(new SchemaInfo() { Version = currentVersion });
                    context.SaveChanges();
                }

                DatabaseContextSeeder.Seed(context, fromRecons, toRecons);
            }
        }
    }
}
