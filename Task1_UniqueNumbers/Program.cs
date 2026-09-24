using System;
using System.Collections.Generic;

namespace Task1_UniqueNumbers;

public class Solution
{
    public int UniqueNumbers(List<int> inputList)
    {
        HashSet<int> numbers = new HashSet<int>(inputList);
        return numbers.Count;
    }
}

public static class Program
{
    public static void Main()
    {
        List<int> inputList = new List<int> { 1, 2, 2, 3, 4, 4, 4 };
        Solution solution = new Solution();

        Console.WriteLine("Input: " + string.Join(", ", inputList));
        Console.WriteLine("Unique numbers: " + solution.UniqueNumbers(inputList));
    }
}
