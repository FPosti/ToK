using System;
string input = Console.ReadLine().ToLower();
string lettersOnly = "";
bool hasPalindrome = false;

foreach (char character in input)
{
    if (char.IsLetter(character))
    {
        lettersOnly = lettersOnly+character;
    }
}


for (int position = 0; position < lettersOnly.Length; position++)
{
    char currentLetter = lettersOnly[position];

    // Check adjacent letters, for example "aa".
    if (position + 1 < lettersOnly.Length)
    {
        char nextLetter = lettersOnly[position + 1];

        if (currentLetter == nextLetter)
        {
            hasPalindrome = true;
            break;
        }
    }

    // Check letters with one letter between them, for example "aba".
    if (position + 2 < lettersOnly.Length)
    {
        char letterTwoStepsAhead = lettersOnly[position + 2];

        if (currentLetter == letterTwoStepsAhead)
        {
            hasPalindrome = true;
            break;
        }
    }
}

if (hasPalindrome)
{
    Console.WriteLine("Palindrome");
}
else
{
    Console.WriteLine("Anti-palindrome");
}