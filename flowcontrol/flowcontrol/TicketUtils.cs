using System;
using System.Collections.Generic;
using System.Text;

namespace flowcontrol
{
    internal static class TicketUtils
    {
        public static int GetTicketPrice(int age)
        {
            if (age < 20)
            {
                return 80;
            }
            else if (age > 64)
            {
                return 90;
            }
            else
            {
                return 120;
            }
        }
    }
}
