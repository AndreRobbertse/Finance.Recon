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
            //Configuration.ProxyCreationEnabled = true;
            //Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ReconContext>(modelBuilder);
            //Database.SetInitializer(sqliteConnectionInitializer);

            ReconContextInitializer initializer = new ReconContextInitializer(modelBuilder);
            //initializer.SetData();
            Database.SetInitializer(initializer);

            var model = modelBuilder.Build(Database.Connection);
            ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
            string sql = sqlGenerator.Generate(model.StoreModel);
            Database.CreateIfNotExists();

            Database.SetInitializer(new ReconContextInitializer(modelBuilder));

        }

        public void Initialize()
        {
            using (ReconContext context = new ReconContext())
            {
                int currentVersion = 0;

                var versions = context.SchemaInfoes.ToList();
                if (versions.Count > 0)
                {
                    currentVersion = context.SchemaInfoes.Max(x => x.Version);
                }
                ContextHelper contextHelper = new ContextHelper();
                while (currentVersion < RequiredDatabaseVersion)
                {
                    currentVersion++;
                    foreach (string migration in contextHelper.Migrations[currentVersion])
                    {
                        context.Database.ExecuteSqlCommand(migration);

                    }
                    context.SchemaInfoes.Add(new SchemaInfo() { Version = currentVersion });
                    context.SaveChanges();
                }
            }
        }

        public void Init(IEnumerable<IRecon> fromRecons, IList<IRecon> toRecons)
        {
            //using (ReconContext context = new ReconContext())
            //{
            //    context.ReconFroms.AddRange(fromRecons);
            //}
        }
    }
}
