
using System;
using System.Threading;

namespace CSharpAdvanced
{
    public class Program
    {
        delegate void User();
        public static void Main(string[] args)
        {

            Thread.CurrentThread.Name = "Main";

            //DelegatesExample.Program.Main();

            //EventsExample.Program.Main();

            //ExtensionMethods.Program.Main();

            //TryMethodPattern.Program.Main();

            //ConcurrentCollections.Program.Main();

            TaskExamples.Program.Main();

            Console.WriteLine("Program has finished...");
            Console.ReadKey();
        }

    }
}
