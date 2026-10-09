namespace ExceptionsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("=== Start av programmet ===");

                // Exempel 1: try-catch-finally
                try
                {
                    Console.WriteLine("Försöker läsa fil och räkna...");
                    var path = "numbers.txt";
                    var result = ProcessFile(path);

                    Console.WriteLine($"\nResultat: {result}");
                }
                catch (FileNotFoundException ex)
                {
                    // Specifikt fel om filen inte finns
                    Console.WriteLine($"Filen hittades inte: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    // Specifikt fel om texten inte kan tolkas som tal
                    Console.WriteLine($"Formatfel: {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    // Specifikt fel om nolldivision
                    Console.WriteLine($"Kan inte dividera med noll: {ex.Message}");
                }
                catch (OverflowException ex)
                {
                    // Specifikt fel om talet är för stort eller för litet
                    Console.WriteLine($"För stort eller för litet tal: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    // Specifikt fel om argumentet till metoden är fel
                    Console.WriteLine($"Felaktigt argument: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    // Specifikt fel om operationen inte går att utföra
                    Console.WriteLine($"Ogiltig operation: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Fallback för alla övriga obekanta fel
                    Console.WriteLine($"Okänt fel: {ex.Message}");
                }
                finally
                {
                    // Körs ALLTID, även om det blev undantag
                    Console.WriteLine("Cleanup: Logging avslutat anrop.");
                }

                Console.WriteLine("Programmet avslutas normalt.");
            }

            // Exempel på metod som själv kastar undantag (throw new)
            static double ProcessFile(string fileName)
            {
                // Om filnamnet är tomt: logiskt fel vi vill signalera
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentException("Filnamn får inte vara tomt eller null.", nameof(fileName));
                }

                // Kontrollerar filen själv och kastar ett tydligt filfel
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("Filen finns inte.", fileName);
                }

                try
                {
                    // using stänger filen automatiskt, även om ett undantag kastas
                    using StreamReader reader = new StreamReader(fileName);

                    string? line = reader.ReadLine();
                    if (line == null)
                    {
                        throw new InvalidOperationException("Filen är tom.");
                    }

                    // Om raden bara innehåller mellanslag finns inget användbart tal
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        throw new InvalidOperationException("Filen innehåller inget tal.");
                    }

                    string text = line.Trim();

                    // Om texten inte kan tolkas som tal kastar vi FormatException
                    if (!int.TryParse(text, out int number))
                    {
                        if (System.Numerics.BigInteger.TryParse(text, out _))
                        {
                            throw new OverflowException("Talet är för stort eller för litet för int.");
                        }

                        throw new FormatException("Texten i filen måste vara ett heltal.");
                    }

                    // 100.0 / 0 ger inte DivideByZeroException, så vi kastar felet själva
                    if (number == 0)
                    {
                        throw new DivideByZeroException("Talet i filen får inte vara 0.");
                    }

                    // Ett negativt tal får fångas av den generella fallback-catchen
                    if (number < 0)
                    {
                        throw new ArithmeticException("Negativa tal stöds inte i den här uträkningen.");
                    }

                    return 100.0 / number;
                }
                catch (FormatException ex)
                {
                    // Vi kan logga felet här, men låter Main hantera det
                    Console.WriteLine($"Formatfel i ProcessFile: {ex.Message}");
                    throw; // Kastar vidare samma exception utan att skapa ett nytt
                }
            }
        }
    }
}
