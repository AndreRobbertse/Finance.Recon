using System;
using System.Threading;

namespace Recon.Shared.Common
{
    public static class ExceptionWriter
    {
        public static void Display(Exception exception)
        {
            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(exception.Message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            (new Thread(delegate ()
            {
                throw new ArgumentException("Failed");
            })).Start();

        }
    }
}