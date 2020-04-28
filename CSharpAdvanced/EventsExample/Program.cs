using System;
using System.Threading;

namespace CSharpAdvanced.EventsExample
{
    public class Program
    {
        public static void Main()
        {
            var broadcaster = new Broadcaster();

            broadcaster.Price = 250;

            var s1 = new Subscriber("S1");
            var s2 = new Subscriber("S2");
            var s3 = new Subscriber("S3");

            broadcaster.PriceChanged += s1.OnPriceChanged;
            broadcaster.PriceChanged += s2.OnPriceChanged;
            broadcaster.PriceChanged += s3.OnPriceChanged;

            Console.WriteLine("Change price to 180$...");

            broadcaster.Price = 180;

            Thread.Sleep(1000);

            Console.WriteLine("Change price to 250$...");

            broadcaster.Price = 250;

        }
    }
}
