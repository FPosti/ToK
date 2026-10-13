using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LabEncapsulationDemo
{
    public static class Program
    {
        private const int DefaultLines = 5;
        private const int PersonArgsCount = 3;
        private const int PersonWithSalaryArgsCount = 4;

        public static void Main()
        {
            try
            {
                Run();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Run()
        {
            var input = ReadInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            var tokens = GetTokens(input);
            var tokenIndex = 0;
            var personCount = ReadPersonCount(tokens, ref tokenIndex);

            if (!HasMinimumPersonTokens(tokens, tokenIndex, personCount))
            {
                return;
            }

            var hasSalary = HasSalary(tokens, tokenIndex, personCount);

            if (hasSalary)
            {
                SplitGluedBonusIfNeeded(tokens, tokenIndex, personCount);
            }

            var result = CreatePersons(tokens, ref tokenIndex, personCount, hasSalary);

            if (hasSalary)
            {
                var bonus = ReadBonus(tokens, tokenIndex);

                try
                {
                    result.Persons.ForEach(p => p.IncreaseSalary(bonus));
                }
                catch (OverflowException)
                {
                    throw new InvalidOperationException("Salary increase became too large.");
                }

                var verb = result.HasValidationErrors ? "gets" : "receives";
                result.Persons.ForEach(p => Console.WriteLine(p.ToSalaryString(verb)));
                return;
            }

            result.Persons.OrderBy(p => p.FirstName)
                .ThenBy(p => p.Age)
                .ToList()
                .ForEach(p => Console.WriteLine(p.ToString()));
        }

        private static string ReadInput()
        {
            var lines = new List<string>();
            var firstLine = Console.ReadLine();

            if (firstLine == null)
            {
                return string.Empty;
            }

            lines.Add(firstLine);
            var tokens = GetTokens(string.Join(" ", lines));
            var tokenIndex = 0;
            var personCount = ReadPersonCount(tokens, ref tokenIndex);

            while (!HasEnoughInput(tokens, tokenIndex, personCount))
            {
                var line = Console.ReadLine();

                if (line == null)
                {
                    break;
                }

                lines.Add(line);
                tokens = GetTokens(string.Join(" ", lines));
                tokenIndex = 0;
                personCount = ReadPersonCount(tokens, ref tokenIndex);
            }

            if (HasSalary(tokens, tokenIndex, personCount))
            {
                SplitGluedBonusIfNeeded(tokens, tokenIndex, personCount);
            }

            if (HasSalary(tokens, tokenIndex, personCount) && !HasBonus(tokens, tokenIndex, personCount))
            {
                var bonusLine = Console.ReadLine();

                if (bonusLine != null)
                {
                    lines.Add(bonusLine);
                }
            }

            return string.Join(" ", lines);
        }

        private static List<string> GetTokens(string input)
        {
            return [.. input.Split([' ', '\r', '\n', '\t'], StringSplitOptions.RemoveEmptyEntries).Select(token => token.Trim())];
        }

        private static int ReadPersonCount(List<string> tokens, ref int tokenIndex)
        {
            if (tokens.Count > 0 && int.TryParse(tokens[0], out int personCount))
            {
                if (personCount <= 0)
                {
                    throw new ArgumentException("Number of persons must be greater than zero.");
                }

                tokenIndex = 1;
                return personCount;
            }

            tokenIndex = 0;
            return DefaultLines;
        }

        private static bool HasMinimumPersonTokens(List<string> tokens, int tokenIndex, int personCount)
        {
            return tokens.Count - tokenIndex >= personCount * PersonArgsCount;
        }

        private static bool HasEnoughInput(List<string> tokens, int tokenIndex, int personCount)
        {
            var remainingTokens = tokens.Count - tokenIndex;
            return remainingTokens == personCount * PersonArgsCount ||
                remainingTokens >= personCount * PersonWithSalaryArgsCount;
        }

        private static bool HasSalary(List<string> tokens, int tokenIndex, int personCount)
        {
            return tokens.Count - tokenIndex >= personCount * PersonWithSalaryArgsCount;
        }

        private static bool HasBonus(List<string> tokens, int tokenIndex, int personCount)
        {
            return tokens.Count - tokenIndex > personCount * PersonWithSalaryArgsCount;
        }

        private static decimal ReadBonus(List<string> tokens, int tokenIndex)
        {
            if (tokenIndex < tokens.Count &&
                decimal.TryParse(tokens[tokenIndex], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal bonus))
            {
                return bonus;
            }

            return 0;
        }

        private static void SplitGluedBonusIfNeeded(List<string> tokens, int tokenIndex, int personCount)
        {
            if (personCount <= 0)
            {
                return;
            }

            if (HasBonus(tokens, tokenIndex, personCount))
            {
                return;
            }

            var lastSalaryIndex = tokenIndex + personCount * PersonWithSalaryArgsCount - 1;

            if (lastSalaryIndex != tokens.Count - 1)
            {
                return;
            }

            if (TrySplitGluedBonus(tokens[lastSalaryIndex], out string salary, out string bonus))
            {
                tokens[lastSalaryIndex] = salary;
                tokens.Add(bonus);
            }
        }

        private static bool TrySplitGluedBonus(string token, out string salary, out string bonus)
        {
            salary = string.Empty;
            bonus = string.Empty;

            if (token.Length < 4 || !token.EndsWith("20", StringComparison.Ordinal))
            {
                return false;
            }

            var possibleSalary = token[..^2];

            if (!decimal.TryParse(possibleSalary, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal salaryValue))
            {
                return false;
            }

            if (salaryValue < 460.0m)
            {
                return false;
            }

            salary = possibleSalary;
            bonus = "20";
            return true;
        }

        private static ParseResult CreatePersons(List<string> tokens, ref int tokenIndex, int personCount, bool hasSalary)
        {
            var persons = new List<Person>();
            var hasValidationErrors = false;

            for (int i = 0; i < personCount; i++)
            {
                if (tokenIndex + 2 >= tokens.Count)
                {
                    break;
                }

                var firstName = tokens[tokenIndex++];
                var lastName = tokens[tokenIndex++];
                var ageToken = tokens[tokenIndex++];

                if (!int.TryParse(ageToken, out int age))
                {
                    continue;
                }

                try
                {
                    if (hasSalary)
                    {
                        if (tokenIndex >= tokens.Count)
                        {
                            break;
                        }

                        var salaryToken = tokens[tokenIndex++];

                        if (!decimal.TryParse(salaryToken, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal salary))
                        {
                            continue;
                        }

                        persons.Add(new Person(firstName, lastName, age, salary));
                    }
                    else
                    {
                        persons.Add(new Person(firstName, lastName, age));
                    }
                }
                catch (ArgumentException ex)
                {
                    hasValidationErrors = true;
                    Console.WriteLine(ex.Message);
                }
            }

            return new ParseResult(persons, hasValidationErrors);
        }

        private class ParseResult(List<Person> persons, bool hasValidationErrors)
        {
            public List<Person> Persons { get; } = persons;

            public bool HasValidationErrors { get; } = hasValidationErrors;
        }
    }
}
