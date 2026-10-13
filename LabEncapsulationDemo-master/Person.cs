using System;
using System.Globalization;

namespace LabEncapsulationDemo
{
    public class Person
    {
        private const string FirstNameErrorMessage = "First name cannot contain fewer than 3 symbols!";
        private const string LastNameErrorMessage = "Last name cannot contain fewer than 3 symbols!";
        private const string AgeErrorMessage = "Age cannot be zero or a negative integer!";
        private const string SalaryErrorMessage = "Salary cannot be less than 460 dollars!";

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private int _age;
        private decimal _salary;
        private readonly bool _hasSalary;

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Person(string firstName, string lastName, int age, decimal salary)
            : this(firstName, lastName, age)
        {
            Salary = salary;
            _hasSalary = true;
        }

        public string FirstName
        {
            get { return _firstName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException(FirstNameErrorMessage);
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException(LastNameErrorMessage);
                }

                _lastName = value;
            }
        }

        public int Age
        {
            get { return _age; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(AgeErrorMessage);
                }

                _age = value;
            }
        }

        public decimal Salary
        {
            get { return _salary; }
            private set
            {
                if (value < 460.0m)
                {
                    throw new ArgumentException(SalaryErrorMessage);
                }

                _salary = value;
            }
        }

        public void IncreaseSalary(decimal percentage)
        {
            if (!_hasSalary)
            {
                return;
            }

            if (Age < 30)
            {
                percentage /= 2;
            }

            Salary += Salary * percentage / 100;
        }

        public override string ToString()
        {
            if (_hasSalary)
            {
                return ToSalaryString("gets");
            }

            return $"{FirstName} {LastName} is {Age} years old.";
        }

        public string ToSalaryString(string verb)
        {
            return $"{FirstName} {LastName} {verb} {Salary.ToString("F2", CultureInfo.InvariantCulture)} dollars.";
        }
    }
}
