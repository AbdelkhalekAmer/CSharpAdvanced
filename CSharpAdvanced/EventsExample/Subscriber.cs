using System;
using System.Threading;

namespace CSharpAdvanced.EventsExample
{
    public class Subscriber
    {
        private readonly string _title;
        public Subscriber(string title)
        {
            _title = title;
        }

        public void OnPriceChanged(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            var data = e as PriceChangedEventArgs;

            var broadcaster = sender as Broadcaster;

            

            Console.WriteLine("\n\n\n============================================================================");
            Console.WriteLine($"Subscriber {_title} is listenning to a message now...");
            Thread.Sleep(2000);
            Console.WriteLine($"Data: {data.LastPrice}$ to {data.NewPrice}$");
            Thread.Sleep(1000);
            Console.WriteLine($"Broadcasted by {broadcaster.Title}");
            Thread.Sleep(1000);
            Console.WriteLine("============================================================================");
        }

    }
}
