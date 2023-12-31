using System;
using System.Linq;

public static class ValidationHelpers
{
    public static bool IsValidName(string input)
    {
        // Validate that the input contains only letters
        return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
    }

    public static bool IsValidPhoneNumber(string input)
    {
        // Validate that the input contains only digits
        return !string.IsNullOrWhiteSpace(input) && input.All(char.IsDigit);
    }

    public static bool IsValidEmail(string input)
    {
        // Validate that the input contains '@'
        return !string.IsNullOrWhiteSpace(input) && input.Contains("@");
    }
}