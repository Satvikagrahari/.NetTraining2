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
    


