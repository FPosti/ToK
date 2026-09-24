using System;
using System.Numerics;

string inputline;

while ((inputline = Console.ReadLine()) != null)
{
    string[] numbertexts = inputline.Split(
        ' ', StringSplitOptions.RemoveEmptyEntries);

    BigInteger smallestmultiple = 1;

    foreach (string numbertext in numbertexts)
    {
        BigInteger currentnumber = BigInteger.Parse(numbertext);

        BigInteger greatestcommondivisor =
            BigInteger.GreatestCommonDivisor(smallestmultiple, currentnumber);

        smallestmultiple =
            (smallestmultiple / greatestcommondivisor) * currentnumber;
    }

    Console.WriteLine(smallestmultiple);
}