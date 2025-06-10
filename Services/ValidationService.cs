using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CarHireSystem.Models;

namespace CarHireSystem.Services
{
    public class ValidationService
    {
        public static bool ValidateInput<T>(string? input, string propertyName, out string? errorMessage)
        {
            errorMessage = string.Empty;
            if (input == null)
            {
                errorMessage = "Input cannot be null";
                return false;
            }

            var context = new ValidationContext(new { Value = input });
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(input, context, results))
            {
                errorMessage = results.First().ErrorMessage;
                return false;
            }

            return true;
        }

        public static bool ValidateCarInput(Car car, out string? errorMessage)
        {
            if (car == null)
            {
                errorMessage = "Car cannot be null";
                return false;
            }

            errorMessage = string.Empty;
            var context = new ValidationContext(car);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(car, context, results, true))
            {
                errorMessage = results.First().ErrorMessage;
                return false;
            }

            return true;
        }

        public static string ReadValidInput(string prompt, Func<string, bool> validationFunc, string invalidMessage)
        {
            string input;
            bool isValid;

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                isValid = validationFunc(input);
                if (!isValid)
                {
                    Console.WriteLine(invalidMessage);
                }
            } while (!isValid);

            return input;
        }

        public static string ReadValidString(string prompt, int minLength, int maxLength, string errorMessage)
        {
            return ReadValidInput(prompt, input => 
                !string.IsNullOrEmpty(input) && 
                input.Length >= minLength && 
                input.Length <= maxLength, 
                errorMessage);
        }

        public static decimal ReadValidDecimal(string prompt, decimal min, decimal max, string errorMessage)
        {
            decimal value;
            string input;
            bool isValid;

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                isValid = decimal.TryParse(input, out value) && value >= min && value <= max;
                if (!isValid)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!isValid);

            return value;
        }

        public static int ReadValidInteger(string prompt, int min, int max, string errorMessage)
        {
            int value;
            string input;
            bool isValid;

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                isValid = int.TryParse(input, out value) && value >= min && value <= max;
                if (!isValid)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!isValid);

            return value;
        }
    }
}
