using System;

namespace CSharpAdvanced.OOP
{
    public static class VirtualAndAbstract
    {
        public abstract class Parent
        {
            public void MethodA()
            {
                Console.WriteLine("Parent invokes MethodA");
            }

            public abstract void MethodB();

            public virtual void MethodC()
            {
                Console.WriteLine("Parent invokes MethodC");
            }
        }

        public class Child : Parent
        {
            public override void MethodB()
            {
                Console.WriteLine("Child invokes MethodB");
            }

            public override void MethodC()
            {
                Console.WriteLine("Child invokes MethodC");
            }
        }
    }
}
