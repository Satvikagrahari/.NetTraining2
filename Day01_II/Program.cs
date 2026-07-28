// Ques6
// H1
// A utility billing department wants a reusable billing calculator where different customer types (Residential and Commercial) calculate bills differently, and input files frequently contain invalid values.
// Design a Console application using multiple classes and interfaces to calculate electricity bills. Accept validated user inputs, apply customer-specific calculation rules, and ensure invalid numeric values do not terminate the application.
// Multiple classes, interfaces, TryParse, arithmetic operators, type conversions, validation, extensibility
// Customer Type, Units Consumed, Rate, Fixed Charges
// Correct bill calculation based on customer type. Invalid inputs should be handled gracefully with meaningful messages.

using System.Runtime.ConstrainedExecution;

interface IBillCalculator
{
    double CalculateBill(double units, double rate, double fixedCharges);
}

class ResidentailCustomer : IBillCalculator
{
    public double CalculateBill(double units, double rate, double fixedCharges)
    {
        return (units * rate) + fixedCharges;
    }
}
class CommercialCustomer : IBillCalculator
{
    public double CalculateBill(double units, double rate, double fixedCharges)
    {
        return (units * rate * (20/100)) + fixedCharges;
    }
}
class Program
{
    public static void Main()
    {
        System.Console.Write("Enter Customer Type (Residential/Commercial): ");
        string customerType = Console.ReadLine().ToLower().Trim();
        System.Console.Write("units: ");
        double units;
        while(!double.TryParse(Console.ReadLine(), out units) || units <= 0)
        {
            System.Console.WriteLine("Enter valid non-neg number");
        }
        System.Console.Write("rate: ");
        double rate;
        while(!double.TryParse(Console.ReadLine(), out rate) || rate <= 0)
        {
            System.Console.WriteLine("Enter valid non-neg number");
        }
        System.Console.Write("fixedCharges: ");
        double fixedCharges;
        while(!double.TryParse(Console.ReadLine(), out fixedCharges) || fixedCharges <= 0)
        {
            System.Console.WriteLine("Enter valid non-neg number");
        }

        IBillCalculator calculator;
        if (customerType == "residential")
        {
            calculator = new ResidentailCustomer();
        }
        else if (customerType == "commercial")
        {
            calculator = new CommercialCustomer();
        }
        else
        {
            System.Console.WriteLine("invalid customer type");
            return;
        }
        System.Console.WriteLine($"Total Amount To Pay = {calculator.CalculateBill(units,rate,fixedCharges)}");
    }
}