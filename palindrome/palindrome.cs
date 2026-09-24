using System;

string s = Console.ReadLine();
bool isPalindrome = true;

for (int i = 0, j = s.Length - 1; i < j; i++, j--)
{
    if (s[i] != s[j])
    {
        isPalindrome = false;
        break;
    }
}

if (isPalindrome)
{
    Console.WriteLine("Palindrome!");
}
else
{
    Console.WriteLine("Nothing special about this string :(");
}