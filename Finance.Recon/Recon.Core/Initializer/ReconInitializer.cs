using System.Collections.Generic;
using System.Data.Entity;
using Recon.Core.Contexts;
using Recon.Interface;
using Recon.Model;
using SQLite.CodeFirst;

namespace Recon.Core.Initializer
{
    public class ReconDropCreateDatabaseAlways : DropCreateDatabaseAlways<ReconContext>
    {
        private IList<IRecon> _fromData;
        private IList<IRecon> _toData;

        public ReconDropCreateDatabaseAlways(IList<IRecon> fromData, IList<IRecon> toData)
        {
            _fromData = fromData;
            _toData = toData;
        }

        protected override void Seed(ReconContext context)
        {
            DatabaseContextSeeder.Seed(context, _fromData, _toData);
        }
    }

    public class ReconDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<ReconContext>
    {
        private IList<IRecon> _fromData;
        private IList<IRecon> _toData;

        public ReconDropCreateDatabaseIfModelChanges(IList<IRecon> fromData, IList<IRecon> toData)
        {
            _fromData = fromData;
            _toData = toData;
        }

        protected override void Seed(ReconContext context)
        {
            DatabaseContextSeeder.Seed(context, _fromData, _toData);
        }
    }

    public class ReconCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<ReconContext>
    {
        private IList<IRecon> _fromData;
        private IList<IRecon> _toData;

        public ReconCreateDatabaseIfNotExists(IList<IRecon> fromData, IList<IRecon> toData)
        {
            _fromData = fromData;
            _toData = toData;
        }

        protected override void Seed(ReconContext context)
        {
            DatabaseContextSeeder.Seed(context, _fromData, _toData);
        }
    }

    public class ReconContextInitializer : SqliteDropCreateDatabaseAlways<ReconContext>
    {
        private IList<IRecon> _fromData;
        private IList<IRecon> _toData;

        public ReconContextInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder) { }

        public void SetData(IList<IRecon> fromData, IList<IRecon> toData)
        {
            _fromData = fromData;
            _toData = toData;
        }

        protected override void Seed(ReconContext context)
        {
            DatabaseContextSeeder.Seed(context, _fromData, _toData);
        }
    }
}