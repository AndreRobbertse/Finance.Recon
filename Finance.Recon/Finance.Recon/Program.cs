using System;
using System.Threading.Tasks;
using Recon.Process.Files;

namespace Finance.Recon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            Console.WriteLine("Press ESC to Exit");
            Console.WriteLine(Environment.NewLine);

            var taskKeys = new Task(ReadKeys);

            var taskProcessFiles = new Task(ProcessImportFiles);
            taskKeys.Start();

            taskProcessFiles.ContinueWith(FilesProcessedComplete);
            taskProcessFiles.Start();

            var tasks = new[] { taskKeys, taskProcessFiles };
            Task.WaitAll(tasks);
        }

        private static void ReadKeys()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("UpArrow was pressed");
                        break;
                    case ConsoleKey.DownArrow:
                        Console.WriteLine("DownArrow was pressed");
                        break;

                    case ConsoleKey.RightArrow:
                        Console.WriteLine("RightArrow was pressed");
                        break;

                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("LeftArrow was pressed");
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                        }
                        break;
                }
            }
        }

        private static void ProcessImportFiles()
        {
            var processFiles = new ProcessFiles();
        }

        private static void FilesProcessedComplete(Task task)
        {
            if (task.IsCompleted)
            {
                Console.WriteLine("Files Processed !");
            }
        }
    }
}
