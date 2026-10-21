using System.Collections;

namespace SortedListExample
{
    internal class Program
    {
        static void Main()
        {
            SortedList sorted = new()
            {
                ["c"] = 3,
                ["a"] = 1,
                ["b"] = 2
            };

            foreach (DictionaryEntry entry in sorted)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }
}
