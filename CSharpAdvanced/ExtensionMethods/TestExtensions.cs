using System;

namespace CSharpAdvanced.ExtensionMethods
{
    public static class TestExtensions
    {
        public static void Print(this String str)
        {
            Console.WriteLine(str);
        }
    }
}
