using static CSharpAdvanced.OOP.VirtualAndAbstract;

namespace CSharpAdvanced.OOP
{
    public class Program
    {
        public static void Main()
        {
            Parent obj = new Child();

            obj.MethodA();
            obj.MethodB();
            obj.MethodC();

        }
    }
}
