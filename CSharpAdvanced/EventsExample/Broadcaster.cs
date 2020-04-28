using System;

namespace CSharpAdvanced.EventsExample
{
    public class Broadcaster
    {
        public string Title { get; } = "Test Broadcaster";

        public event EventHandler<PriceChangedEventArgs> PriceChanged = delegate { };
        //public event EventHandler<PriceChangedEventArgs> PriceChanged = (sender, e) => { }; // this will work fine also

        private int price;

        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price == value) return;

                OnPriceChanged(new PriceChangedEventArgs(price, value));
                price = value;
            }
        }


        public Broadcaster()
        {

        }

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            if (e.NewPrice > 200)
                PriceChanged.Invoke(this, e); // removed null check because the object is already initialized.

        }



    }
}
