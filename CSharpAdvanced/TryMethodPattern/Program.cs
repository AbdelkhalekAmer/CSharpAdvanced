using CSharpAdvanced.CommonModels;

using System;

namespace CSharpAdvanced.TryMethodPattern
{
    public class Program
    {
        public static void Main()
        {
            var p = new Parent() { A = "data" };

            if (p.TryPrint(out string data))
            {
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
