using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recon.Core.Contexts;
using Recon.Interface;
using Recon.Model;
using Recon.Shared.Common;

namespace Recon.Core.Initializer
{
    public static class DatabaseContextSeeder
    {
        private static int totalRows = 0;
        private static int rowsDone = 0;

        public static void Seed(ReconContext context, IList<IRecon> fromData, IList<IRecon> toData)
        {
            var start = DateTime.Now;
            Console.WriteLine("Saving {0} from recons", fromData.Count);
            Console.WriteLine("Saving {0} to recons", toData.Count);
            var step = ReconType.From;

            int iFrom = 0;
            int iTo = 0;
            totalRows = fromData.Count + toData.Count;

            ProgressBar(rowsDone, totalRows);
            IRecon problemKid = null;
            try
            {
                foreach (var fromRecon in fromData)
                {
                    problemKid = fromRecon;
                    context.ReconFroms.Add((ReconFrom)fromRecon);
                    context.SaveChanges();
                    iFrom++;
                    rowsDone++;
                    ProgressBar(rowsDone, totalRows);
                }
                step = ReconType.To;
                foreach (var toRecon in toData)
                {
                    problemKid = toRecon;
                    context.ReconTos.Add((ReconTo)toRecon);
                    context.SaveChanges();
                    iTo++;
                    rowsDone++;
                    ProgressBar(rowsDone, totalRows);
                }

                ProgressBar(totalRows, totalRows);
                TimeSpan duration = DateTime.Now - start;
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Seeder Complete (Taken {0} seconds)", duration.TotalSeconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Type:{0} - Row:{1}", step, (step == ReconType.From) ? iFrom : iTo);
                if (problemKid != null)
                {
                    Console.WriteLine(problemKid.Id + ":" + problemKid.Amount);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private static void ProgressBar(int progress, int tot)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / tot;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + tot.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}