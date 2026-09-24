using System;
int n = int.Parse(Console.ReadLine());

int skrivare = 1;
int dagar = 0;

while (skrivare < n)
{
    skrivare = skrivare * 2;
    dagar++;
}

Console.WriteLine(dagar + 1);