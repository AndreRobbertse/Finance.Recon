using System.Collections.Generic;
using Recon.Core.Contexts;
using Recon.Interface;
using Recon.Model;

namespace Recon.Core.Initializer
{
    public static class DatabaseContextSeeder
    {
        public static void Seed(ReconContext context, IList<IRecon> fromData, IList<IRecon> toData)
        {
            if (fromData != null)
            {
                foreach (var fromRecon in fromData)
                {
                    context.ReconFroms.Add((ReconFrom)fromRecon);
                }
            }

            if (toData != null)
            {
                foreach (var toRecon in toData)
                {
                    context.ReconTos.Add((ReconTo)toRecon);
                }
            }
        }
    }
}