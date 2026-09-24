using System;

class Program
{
    static void Main()
    {   
        Console.WriteLine("Enter a word:");
        string word = Console.ReadLine();
        Console.WriteLine("Enter the number");
        int times = int.Parse(Console.ReadLine());

        for (int i = 0; i < times; i++)
        {
            Console.Write(word);
        }

        Console.WriteLine();
    }
}