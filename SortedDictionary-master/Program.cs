namespace SortedDictionary
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// SortedDictionary sorterar automatiskt
    /// </summary>
    class Program
    {
        static void Main()
        {
            var ages = new SortedDictionary<string, int>();

            ages["Karin"] = 42;
            ages["Anna"] = 50;
            ages["Björn"] = 35;

            foreach (var item in ages)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
