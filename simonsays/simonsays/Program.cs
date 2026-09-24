using System;
Console.WriteLine("welcome to the Simon Says game!");
Console.WriteLine("Enter the number of commands:");
int numberOfCommands = int.Parse(Console.ReadLine());
string prefix = "simon says ";

for (int index = 0; index < numberOfCommands; index++)
{
    string command = Console.ReadLine();

    if (command.StartsWith(prefix))
    {
        string action = command.Substring(prefix.Length);
        Console.WriteLine(action);
    }
    else
    {
        Console.WriteLine();
    }
}