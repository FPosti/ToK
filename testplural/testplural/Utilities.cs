using System;
using System.Collections.Generic;
using System.Text;

namespace testplural
{
    internal class Utilities
    {
        public static int calculateyearlywage(int monthlywage, int numberofmonthsworked)
        {
            int local = 100;
            if (numberofmonthsworked == 12)
            {
                return monthlywage * (numberofmonthsworked + 1);
            }
            else
            {
                Console.WriteLine("You did not work the full year.");
                return monthlywage * numberofmonthsworked;
            }
        }



        public static int calculateyearlywage(int monthlywage, int numberofmonthsworked, int bonus)
        {
            if (numberofmonthsworked == 12)
            {
                return monthlywage * (numberofmonthsworked + 1) + bonus;
            }
            else
            {
                Console.WriteLine("You did not work the full year.");
                return monthlywage * numberofmonthsworked;
            }
        }



    }
}
