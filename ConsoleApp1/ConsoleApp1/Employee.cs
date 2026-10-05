using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Employee(int id, string name, int salary)
    {
        public int Id { get; } = id;
        public string Name { get; set; } = name;
        public int Salary { get; set; } = salary;
    }
}