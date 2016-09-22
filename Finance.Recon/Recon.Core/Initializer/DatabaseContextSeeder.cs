using System;
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
            Console.WriteLine("Saving {0} from recons", fromData.Count);
            Console.WriteLine("Saving {0} to recons", toData.Count);
            try
            {
                foreach (var fromRecon in fromData)
                {
                    context.ReconFroms.Add((ReconFrom)fromRecon);
                    context.SaveChanges();
                }
                foreach (var toRecon in toData)
                {
                    context.ReconTos.Add((ReconTo)toRecon);
                    context.SaveChanges();
                }
                Console.WriteLine("Seeder Complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}