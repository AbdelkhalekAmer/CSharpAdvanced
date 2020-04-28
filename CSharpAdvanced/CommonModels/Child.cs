using System;

namespace CSharpAdvanced.CommonModels
{
    public class Child : Parent, IDisposable
    {
        private bool _isDisposed = false;
        public Child()
        {

        }

        ~Child()
        {
            Console.WriteLine("Child is finalizing...");
            Dispose(false);
        }

        public override void Print()
        {
            Console.WriteLine("Child print method.");
        }

        public void Dispose()
        {
            Console.WriteLine("Child is disposing...");
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            Console.WriteLine("Child dispose action...");

            if (isDisposing)
            {
                // managed code dispose
            }

            // release all un-managed resources owned by (just) this object

            _isDisposed = true;
        }
    }
}
