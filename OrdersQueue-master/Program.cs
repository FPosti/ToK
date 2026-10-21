namespace OrdersQueue
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Exemplet visar en beställningskö med Queue
    /// </summary>
    
    class Program
    {
        static void Main()
        {
            Queue<string> ordinaryOrders = new Queue<string>();
            Queue<string> expressOrders = new Queue<string>();

            // Lägger till beställningar
            ordinaryOrders.Enqueue("Pizza1");   
            ordinaryOrders.Enqueue("Pasta2");   
            ordinaryOrders.Enqueue("Sallad3");
            ordinaryOrders.Enqueue("Taco4");
            ordinaryOrders.Enqueue("Burger5");

            // Lägger till express-beställningar
            expressOrders.Enqueue("VIP-Sallad");
            expressOrders.Enqueue("VIP-Burgare");
            expressOrders.Enqueue("VIP-Pasta");

            Console.WriteLine("Vanliga beställningar:");
            while (ordinaryOrders.Count > 0)
            {
                string order = ordinaryOrders.Dequeue();
                Console.WriteLine(order);
            }

            Console.WriteLine();
            Console.WriteLine("Expressbeställningar:");
            while (expressOrders.Count > 0)
            {
                Console.WriteLine(expressOrders.Peek());
                expressOrders.Dequeue();
            }

            int totalOrders = 5 + 3;
            Console.WriteLine();
            Console.WriteLine($"Totalt: {totalOrders} beställningar");
        }
    }
}
