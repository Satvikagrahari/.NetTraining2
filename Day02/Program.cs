// Scenario Description
// You are building a financial calculator that needs to handle various compound interest calculations for different clients. Some clients have different compounding frequencies, and the method should be flexible to handle common defaults.
// Programming Task / Question
// Design a static class FinancialCalculator with an overloaded static method CalculateCompoundInterest. The method should calculate the future value of an investment. Implement overloads that allow the caller to specify the principal, rate, time, and compounding frequency, but with sensible defaults for time (years) and compounding frequency (annually). Explain how you would use named arguments to make the calling code more readable when you only want to change the compounding frequency.
// Expected Concepts Tested
// Static methods, method overloading, default parameter values, named arguments.
// Input (if applicable)
// Method Call 1: CalculateCompoundInterest(10000, 0.05, 10) 
// Method Call 2: CalculateCompoundInterest(10000, 0.05, 10, compoundingFrequency: 12)
// Expected Output / Behavior
// Output 1: Future value = $16,288.95 (compounded annually) <br> **Output 2:** Future value = $16,470.09 (compounded monthly) 
// Named arguments make the second call clear: CalculateCompoundInterest(principal: 10000, rate: 0.05, time: 10, compoundingFrequency: 12)

// static class FinancialCalculator{
//     public static double CalculateCompoundInterest(double p, double r, double t)
//     {
//         return p*Math.Pow(1+r,t);
//     }
//     public static double CalculateCompoundInterest(double p, double r, double t, double n)
//     {
//         return p*Math.Pow(1+(r/n), n*t);
//     }
// }
// class Program
// {
//     public static void Main()
//     {
//         Console.WriteLine($"Future Value = {FinancialCalculator.CalculateCompoundInterest(10000, 0.05, 10):F2}");
//         Console.WriteLine($"Future Value = {FinancialCalculator.CalculateCompoundInterest(p: 10000, r: 0.05, t: 10, n: 12):F2}");
        
//     }
// }
    


// QUESTION M2
// Element
// Details
// Topic
// static methods, parameters, overloading, default/named args, ref/out/in/params, local functions, recursion, TryParse
// Difficulty Level
// Medium
// Question ID
// M2
// Scenario Description
// You are developing a console application for a library that needs to process book orders. The input is a comma-separated string of ISBNs, and you need to validate each one.
// Programming Task / Question
// Create a static method TryProcessOrder that accepts a string of comma-separated ISBNs using the params keyword. The method should attempt to parse each ISBN (using a simple TryParse-like logic, 
// e.g., checking if it's exactly 13 characters). You must use the out parameter to return a list of valid ISBNs. Describe how you would handle the parsing logic using TryParse-style methods to avoid exceptions.
// Expected Concepts Tested
// params keyword, out parameters, TryParse pattern, method design for validation.
// Input (if applicable)
// TryProcessOrder("978-3-16-148410-0, 1234567890123, invalid-isbn, 978-1-4028-9462-6")
// Expected Output / Behavior
// Returns: true 
// out list contains: ["9783161484100", "9781402894626"] 
// Invalid entries are skipped without throwing exceptions. Each ISBN is validated using a TryParseISBN method that returns bool and uses out string for the cleaned version.

using System;
using System.Collections.Generic;

class Program
{
    
    public static bool TryParseISBN(string input, out string cleanedISBN)
    {
        
        cleanedISBN = input.Replace("-", "").Replace(" ", "");

       
        if (cleanedISBN.Length == 13 && long.TryParse(cleanedISBN, out _))
        {
            return true;
        }

        cleanedISBN = string.Empty;
        return false;
    }


    public static bool TryProcessOrder(out List<string> validISBNs, params string[] isbnGroups)
    {
        validISBNs = new List<string>();

        foreach (string group in isbnGroups)
        {
            string[] isbns = group.Split(',');

            foreach (string isbn in isbns)
            {
                if (TryParseISBN(isbn.Trim(), out string cleaned))
                {
                    validISBNs.Add(cleaned);
                }
            }
        }

        return validISBNs.Count > 0;
    }

    static void Main()
    {
        bool result = TryProcessOrder(
            out List<string> validBooks,
            "978-3-16-148410-0, 1234567890123, invalid-isbn, 978-1-4028-9462-6"
        );

        Console.WriteLine("Result: " + result);

        Console.WriteLine("Valid ISBNs:");

        foreach (string isbn in validBooks)
        {
            Console.WriteLine(isbn);
        }
    }
}