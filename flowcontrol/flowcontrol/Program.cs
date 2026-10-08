using flowcontrol;
var running = true;
while (running)
{
    Console.WriteLine("Välkommen till huvudmenyn!");
    Console.WriteLine("1. Singel biljett");
    Console.WriteLine("2. Flera biljetter");
    Console.WriteLine("3. Upprepa text tio gånger");
    Console.WriteLine("4. Visa tredje ordet");
    Console.WriteLine("0. Avsluta");
    Console.Write("Välj genom att skriva en siffra: ");
    string? choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            running = false;
            break;
        case "1":
            {
                Console.Write("Skriv in din ålder: ");
                string input = Console.ReadLine()!;
                int age = int.Parse(input);
                int price = TicketUtils.GetTicketPrice(age);
                if (age < 20)
                {
                    Console.WriteLine($"Ungdomspris: {price} kr");
                }
                else
                {
                    if (age > 64)
                    {
                        Console.WriteLine($"Pensionärspris: {price} kr");
                    }
                    else
                    {
                        Console.WriteLine($"Standardpris: {price} kr");
                    }
                }
                break;
            }
        case "2":
            {
                Console.Write("Hur många biljetter vill du köpa? ");
                string input = Console.ReadLine()!;
                // omvandlar str input till int
                int tickets = int.Parse(input);
                int total = 0;
                for (int i = 0; i < tickets; i++)
                {
                    Console.Write($"Skriv in åldern på person {i + 1}: ");
                    string ageinput = Console.ReadLine()!;
                    int age = int.Parse(ageinput);
                    // Create a new Customer object for each person
                    Customer customer = new(i, age);
                    int price = TicketUtils.GetTicketPrice(customer.Age);
                    Console.WriteLine($"Biljettpris: {price} kr");
                    total += price;
                }
                Console.WriteLine($"Antal personer: {tickets}");
                Console.WriteLine($"Totalkostnad: {total} kr");
                break;
            }
        case "3":
            {
                Console.Write("Skriv en text: ");
                string text = Console.ReadLine()!;
                // loopar igenom text 10 ggr
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{i + 1}. {text} ");
                }
                Console.WriteLine();
                break;
            }
        case "4":
            {
                while (true)
                {
                    Console.Write("Skriv en mening med minst tre ord: ");
                    // Read the input and handle null case
                    string sentence = Console.ReadLine() ?? "";
                    // skapas en lista iom att .split returnerar en string[]
                    var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length < 3)
                    {
                        Console.WriteLine("För få ord, Försök igen.");
                    }
                    else
                    {
                        // skriveru t det tredje elemenetet i listan.
                        Console.WriteLine($"Det tredje ordet är: {words[2]}");
                        break;
                    }
                }
                break;
            }
        default:
            Console.WriteLine("Ogiltigt alternativ. Försök igen.");
            break;
    }
}