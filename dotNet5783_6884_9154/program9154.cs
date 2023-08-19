using System;

partial public class Class1
{
	public Class1()
	{
		Welcome();

	}
    private static void Welcome()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", name);
    }
}
