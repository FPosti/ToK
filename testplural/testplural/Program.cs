using testplural;


int yearlyWage = Utilities.calculateyearlywage(1234, 12);
int tmp = Utilities.calculateyearlywage(1234, 12, 500);


Console.WriteLine($"Yearly wage with bonus is: {tmp}");
Console.WriteLine($"Yearly wage is: {yearlyWage}");