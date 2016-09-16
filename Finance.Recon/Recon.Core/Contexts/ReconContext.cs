using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Recon.Model;

namespace Recon.Core.Contexts
{
    public class ReconContext : DbContext
    {
        public ReconContext()
            : base(Util.SqliteConnectionString)
        {
        }

        public DbSet<Model.Recon> ReconFroms { get; set; }
        public DbSet<Model.Recon> ReconTos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}