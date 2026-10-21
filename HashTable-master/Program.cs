namespace HashTable
{
    using System;
    using System.Collections;


    class Program
    {
        static void Main()
        {
            // Skapa en Hashtable
            Hashtable ht = new Hashtable();

            // Lägg till nyckel-värde-par (nycklar och värden är object)
            ht["Michael"] = 42;     // Ålder
            ht["Malmö"] = "Skåne";   // Stad och region
            ht["C#"] = "Kurs";

            // Hämta värden (kräver casting)
            //Console.WriteLine((int)ht["Michael"]); // Output: 42
            Console.WriteLine(ht["Michael"] as int?); // Output: 42
            //Console.WriteLine((string)ht["Malmö"]); // Output: Skåne
            Console.WriteLine(ht["Malmö"] as string); // Output: Skåne
                                                    // Enumerera genom samlingen
            foreach (object key in ht.Keys)
            {
                Console.WriteLine($"{key}: {ht[key]}");
            }
        }
    }
}
