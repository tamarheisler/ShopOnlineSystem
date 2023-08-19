using System;

namespace Targil0 {
   partial class Program
    {
        static void Main(String[] args)
        {
            Welcome6884();
            Welcome9154();
            Console.ReadKey();

        }

        static partial void Welcome9154();
        private static void Welcome6884()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
