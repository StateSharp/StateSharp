using System;

namespace GeneratedApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = new Foo();
            Console.WriteLine("Types in this assembly:");
            foreach (Type t in typeof(Program).Assembly.GetTypes())
            {
                Console.WriteLine(t.FullName);
            }
        }
    }
}
