using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Recon.Process.Files;

namespace Finance.Recon
{
    class Program
    {
        const int SWP_NOZORDER = 0x4;
        const int SWP_NOACTIVATE = 0x10;
        int r = 0;

        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        static void Main(string[] args)
        {
            (new Program()).Run();
        }

        void Run()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Console.Title = "Finance Recons";
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

        private void ReadKeys()
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

        private void ProcessImportFiles()
        {
            var processFiles = new ProcessFiles();
        }

        private void FilesProcessedComplete(Task task)
        {
            if (task.IsCompleted)
            {
                if (!task.IsFaulted)
                {
                    Console.WriteLine("Process Complete");
                }
                else
                {
                    Console.WriteLine("Fault found during process task");
                }
            }
        }

        /// <summary>
        /// Sets the console window location and size in pixels
        /// </summary>
        public void SetWindowPosition(int x, int y, int width, int height)
        {
            SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        public static IntPtr Handle
        {
            get
            {
                //Initialize();
                return GetConsoleWindow();
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Interlocked.Increment(ref r);
            Console.WriteLine("Terminating " + e.IsTerminating.ToString());

            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Name = "Dead thread";

            Console.ReadKey();
            //Process.GetCurrentProcess().Kill();
        }
    }
}
