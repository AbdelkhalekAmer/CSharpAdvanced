using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CSharpAdvanced.ConcurrentCollections
{
    public class Program
    {
        public static void Main()
        {

            Run(ConcurrentDictionaryExample.RunExample2, 10);

        }

        private static void Run(Action action, int trials = 1)
        {
            var currentTrial = 0;
            var elapsedTimes = new long[trials];

            Thread.CurrentThread.Name = "Main";
            var watch = Stopwatch.StartNew();

            while (currentTrial < trials)
            {

                action();

                elapsedTimes[currentTrial] = watch.ElapsedMilliseconds;

                watch.Restart();

                currentTrial++;
            }

            watch.Stop();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Average execution time = {Math.Floor(elapsedTimes.Average())} ms.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
