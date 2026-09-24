using System;
using System.Linq;

namespace FairGrading;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter the grades separated by spaces (e.g., 100 90 80):");
        int[] grades = Console.ReadLine()!
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        int x = grades[0];
        int y = grades[1];
        int z = grades[2];

        int weightedScore = x + y + (2 * z);

        char letterGrade = weightedScore >= 360 ? 'A'
            : weightedScore >= 320 ? 'B'
            : weightedScore >= 280 ? 'C'
            : weightedScore >= 240 ? 'D'
            : 'F';

        Console.WriteLine(letterGrade);
    }
}


