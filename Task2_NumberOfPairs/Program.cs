using System;
using System.Collections.Generic;

namespace Task2_NumberOfPairs;

public class Solution
{
    public int NumberOfPairs(List<int> inputList)
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();

        foreach (int number in inputList)
        {
            if (counts.ContainsKey(number))
            {
                counts[number]++;
            }
            else
            {
                counts[number] = 1;
            }
        }

        int pairCount = 0;

        foreach (KeyValuePair<int, int> entry in counts)
        {
            if (entry.Value == 2)
            {
                pairCount++;
            }
        }

        return pairCount;
    }
}

public static class Program
{
    public static void Main()
    {
        List<int> inputList = new List<int> { 1, 2, 2, 3, 4, 4, 4, 5, 5 };
        Solution solution = new Solution();

        Console.WriteLine("Input: " + string.Join(", ", inputList));
        Console.WriteLine("Number of pairs: " + solution.NumberOfPairs(inputList));
    }
}
