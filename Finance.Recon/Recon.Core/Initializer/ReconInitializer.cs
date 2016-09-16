using System.Collections.Generic;
using System.Data.Entity;
using Recon.Core.Contexts;
using Recon.Interface;

namespace Recon.Core.Initializer
{
    public class ReconInitializer : DropCreateDatabaseAlways<ReconContext>
    {
        private readonly IList<IRecon> _fromData;
        private readonly IList<IRecon> _toData;

        public ReconInitializer(IList<IRecon> fromData, IList<IRecon> toData)
        {
            _fromData = fromData;
            _toData = toData;

            // Start DB
            var setup = new Setup();

            //http://blog.devart.com/entity-framework-code-first-support-for-oracle-mysql-postgresql-and-sqlite.html
        }

        protected override void Seed(ReconContext context)
        {
            if (_fromData != null)
            {
                foreach (var fromRecon in _fromData)
                {
                    context.ReconFroms.Add((Model.Recon)fromRecon);
                }
            }

            if (_toData != null)
            {
                foreach (var toRecon in _toData)
                {
                    context.ReconTos.Add((Model.Recon)toRecon);
                }
            }
            base.Seed(context);
        }
    }
}