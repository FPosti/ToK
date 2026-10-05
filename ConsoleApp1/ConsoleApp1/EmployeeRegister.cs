using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class EmployeeRegister
    {
        private readonly List<Employee> employees = [];
        private int idIterator = 1;

        public void AddEmployee(string name, int salary)
        {
            if (salary < 0)
            {
                Console.WriteLine("Salary cannot be negative.");
                return;
            }

            var employee = new Employee(idIterator, name, salary);
            employees.Add(employee);
            idIterator++;
        }

        public bool EditEmployee(int id, string newName, int newSalary)
        {
            if (newSalary < 0)
            {
                Console.WriteLine("Salary cannot be negative.");
                return false;
            }

            foreach (var employee in employees)
            {
                if (employee.Id == id)
                {
                    employee.Name = newName;
                    employee.Salary = newSalary;
                    return true;
                }
            }

            return false;
        }

        public bool DeleteEmployee(int id)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Id == id)
                {
                    employees.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public void PrintRegister()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("The register is empty.");
                return;
            }

            foreach (var employee in employees)
            {
                Console.WriteLine("ID: " + employee.Id +
                                  ", Name: " + employee.Name +
                                  ", Salary: " + employee.Salary);
            }
        }
    }
}
