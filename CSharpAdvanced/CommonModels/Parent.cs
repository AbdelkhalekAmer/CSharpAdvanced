using System;

namespace CSharpAdvanced.CommonModels
{
    public class Parent
    {
        public string a;
        public string A
        {
            get
            {
                if (string.IsNullOrEmpty(a) || string.IsNullOrWhiteSpace(a))
                {
                    throw new MemberAccessException("A is empty.");
                }

                return a;
            }
            set
            {
                a = a == value ? a : value;
            }
        }

        public Parent()
        {

        }

        public virtual void Print()
        {
            Console.WriteLine("Parent print method.");
        }

        public virtual bool TryPrint(out string data)
        {
            try
            {
                data = ToString();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                data = string.Empty;
                return false;
            }
        }

        public void NotImplementedMethod()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var data = $"{typeof(Parent).Name}: property A = {A};";
            return data;
        }

    }
}
