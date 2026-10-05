using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            var register = new EmployeeRegister();
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("1. Add employee");
                Console.WriteLine("2. Edit employee");
                Console.WriteLine("3. Delete employee");
                Console.WriteLine("4. Print register");
                Console.WriteLine("5. Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine()!;

                        Console.Write("Salary: ");
                        int salary = int.Parse(Console.ReadLine()!);

                        register.AddEmployee(name, salary);
                        break;

                    case "2":
                        Console.Write("ID to edit: ");
                        int editId = int.Parse(Console.ReadLine()!);

                        Console.Write("New name: ");
                        string newName = Console.ReadLine()!;

                        Console.Write("New salary: ");
                        int newSalary = int.Parse(Console.ReadLine()!);

                        if (!register.EditEmployee(editId, newName, newSalary))
                        {
                            Console.WriteLine("Employee not found.");
                        }

                        break;

                    case "3":
                        Console.Write("ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine()!);

                        if (!register.DeleteEmployee(deleteId))
                        {
                            Console.WriteLine("Employee not found.");
                        }

                        break;

                    case "4":
                        register.PrintRegister();
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}