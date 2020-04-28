using CSharpAdvanced.CommonModels;

using System;

namespace CSharpAdvanced.DelegatesExample
{
    public class Program
    {
        delegate int Transform(int x);

        delegate T1 Transform<out T1, in T2>(T2 x)
            where T1 : class
            where T2 : Parent;

        public static void Main()
        {
            Transform transform = Doubler;

            // Lambda Expression
            transform += (int x) =>
            {
                Console.WriteLine($"{x} is Tripled");
                return x * 3;
            };

            // Anonymous Method
            transform += delegate (int x)
            {
                Console.WriteLine($"{x} is quadrupled.");
                return x * 4;
            };

            var result = transform(3);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Target is : {transform.Target}");

        }

        private static int Doubler(int x)
        {
            Console.WriteLine($"{x} is doubbled");
            return x * 2;
        }
    }
}
