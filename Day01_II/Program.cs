// Ques6
// H1
// A utility billing department wants a reusable billing calculator where different customer types (Residential and Commercial) calculate bills differently, and input files frequently contain invalid values.
// Design a Console application using multiple classes and interfaces to calculate electricity bills. Accept validated user inputs, apply customer-specific calculation rules, and ensure invalid numeric values do not terminate the application.
// Multiple classes, interfaces, TryParse, arithmetic operators, type conversions, validation, extensibility
// Customer Type, Units Consumed, Rate, Fixed Charges
// Correct bill calculation based on customer type. Invalid inputs should be handled gracefully with meaningful messages.

using System.Runtime.ConstrainedExecution;
// interface IBillCalculator
// {
//     double CalculateBill(double units, double rate, double fixedCharges);
// }

// class ResidentailCustomer : IBillCalculator
// {
//     public double CalculateBill(double units, double rate, double fixedCharges)
//     {
//         return (units * rate) + fixedCharges;
//     }
// }
// class CommercialCustomer : IBillCalculator
// {
//     public double CalculateBill(double units, double rate, double fixedCharges)
//     {
//         return (units * rate * (20/100)) + fixedCharges;
//     }
// }
// class Program
// {
//     public static void Main()
//     {
//         System.Console.Write("Enter Customer Type (Residential/Commercial): ");
//         string customerType = Console.ReadLine().ToLower().Trim();
//         System.Console.Write("units: ");
//         double units;
//         while(!double.TryParse(Console.ReadLine(), out units) || units <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-neg number");
//         }
//         System.Console.Write("rate: ");
//         double rate;
//         while(!double.TryParse(Console.ReadLine(), out rate) || rate <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-neg number");
//         }
//         System.Console.Write("fixedCharges: ");
//         double fixedCharges;
//         while(!double.TryParse(Console.ReadLine(), out fixedCharges) || fixedCharges <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-neg number");
//         }

//         IBillCalculator calculator;
//         if (customerType == "residential")
//         {
//             calculator = new ResidentailCustomer();
//         }
//         else if (customerType == "commercial")
//         {
//             calculator = new CommercialCustomer();
//         }
//         else
//         {
//             System.Console.WriteLine("invalid customer type");
//             return;
//         }
//         System.Console.WriteLine($"Total Amount To Pay = {calculator.CalculateBill(units,rate,fixedCharges)}");
//     }
// }


// Ques 7
// H2
// A payroll system imports employee working hours entered manually by HR. Decimal hours, overtime, and invalid numeric entries must be handled without affecting payroll processing.
// Create a Console-based payroll calculator using separate classes for Employee and PayrollCalculator. Validate all inputs using TryParse, calculate regular pay, overtime pay, and gross salary while handling incorrect or extreme values.
// Multiple classes, object-oriented design, primitive types, conversions, arithmetic operators, validation, Math.Round()
// Employee Name, Hours Worked, Hourly Rate
// Correct salary calculation with overtime rules. Invalid values should be rejected without application failure.

// class Employee
// {
//     public string ?EmpName { get; set; }
//     public decimal HoursWorked { get; set; }
//     public decimal HourlyRate { get; set; }
// }
// class PayrollCalculator
// {
//     public decimal RegularPayCalc(Employee emp)
//     {
//         decimal regularHrs = Math.Min(emp.HoursWorked, 40);
//         return regularHrs * emp.HourlyRate;
//     }
//     public decimal OvertimePayCalc(Employee emp)
//     {
//         if (emp.HoursWorked > 40)
//         {
//             decimal overtimeHrs = emp.HoursWorked - 40;
//             return overtimeHrs * emp.HourlyRate * (decimal)1.5;
//         }
//         return 0;
//     }
//     public decimal GrossPayCalc(Employee emp)
//     {
//         return RegularPayCalc(emp) + OvertimePayCalc(emp);
//     }
// }
// class Program
// {
//     public static void Main()
//     {
//         Employee emp = new Employee();
//         System.Console.Write("Enter Employee Name: ");
//         emp.EmpName = Console.ReadLine();

//         System.Console.Write("Enter hours worked (between 0 to 200): ");
//         decimal hoursWorked;
//         while (!decimal.TryParse(Console.ReadLine(), out hoursWorked) || hoursWorked < 0 || hoursWorked > 200)
//         {
//             System.Console.Write("Enter valid hours worked (between 0 to 200): ");

//         }
//         emp.HoursWorked = hoursWorked;

//         System.Console.Write("Enter Hourly Rate: ");
//         decimal rate;
//         while (!decimal.TryParse(Console.ReadLine(), out rate) || rate < 0)
//         {
//             System.Console.Write("Enter valid Hourly Rate: ");

//         }
//         emp.HourlyRate = rate;
//         PayrollCalculator calc = new PayrollCalculator();
//         System.Console.WriteLine("PAYROLL BILL CALCULATOR");
//         System.Console.WriteLine($"Regular Pay = Rs {calc.RegularPayCalc(emp):F2}");
//         System.Console.WriteLine($"Overtime Pay = Rs {calc.OvertimePayCalc(emp):F2}");
//         System.Console.WriteLine($"Regular Pay = Rs {calc.GrossPayCalc(emp):F2}");
//     }
// }



// Ques 8
// H3
// A logistics company calculates package shipping charges using different pricing strategies based on package type. Incorrect weight and distance values frequently occur.
// Build a Console application using an interface-based design where different package types calculate shipping cost differently. Validate numeric inputs, prevent negative or overflow-prone calculations, and display the final shipping cost.
// Interfaces, multiple classes, arithmetic operators, numeric conversions, validation, TryParse, correctness
// Package Type, Weight, Distance
// Display shipping cost according to package type. Reject invalid or unreasonable input values.

// interface ILogisticCalculator
// {
//     double ShippingCostCalculator(double weight, double distance);
// }
// class NormalPackage() : ILogisticCalculator
// {
//     public double ShippingCostCalculator(double weight, double distance)
//     {
//         return weight * distance * 20;
//     }
// }
// class PriorityPackage() : ILogisticCalculator
// {
//    public double ShippingCostCalculator(double weight, double distance)
//     {
//         return weight * distance * 50;
//     }
// }
// class Program
// {
//     public static void Main()
//     {
//         System.Console.Write("Enter package type Normal/Priority: ");
//         string PackageType = Console.ReadLine().Trim().ToLower();

//         double weight;
//         System.Console.Write("Enter weight(kg): ");
//         while(!double.TryParse(Console.ReadLine(), out weight) || weight <= 0)
//         {
//             System.Console.WriteLine("Enter valid weight in kg: ");
//         }
//         double distance;
//         System.Console.Write("Enter distance(km): ");
//         while(!double.TryParse(Console.ReadLine(), out distance) || distance <= 0)
//         {
//             System.Console.WriteLine("Enter valid distance in km: ");
//         }
//         ILogisticCalculator logisticCalculator;
//         if(PackageType == "normal")
//         {
//             logisticCalculator  = new NormalPackage();
//         }
//         else if (PackageType == "priority")
//         {
//             logisticCalculator = new PriorityPackage();
//         }
//         else
//         {
//             System.Console.WriteLine("Wrong Package Type");
//             return;
//         }
//         double shippingCost = logisticCalculator.ShippingCostCalculator(weight,distance);
//         System.Console.WriteLine($"Shipping Cost = Rs {shippingCost}");
//     }
// }

//Ques9
// H4
// A hospital registration system records patient age, weight, height, and body temperature. Since data is entered manually, the system must remain reliable even when multiple invalid values are entered consecutively.
// Design a Console application with separate validation and patient classes. Continuously prompt until all values are valid, perform necessary numeric conversions, calculate BMI, and display a formatted patient summary.
// Classes, validation layer, Parse/TryParse, variables, primitive types, conversions, Math, user input handling
// Age, Weight, Height, Temperature
// Application should never terminate due to invalid input and should display a validated patient summary with calculated BMI.

// class Patient
// {
//     public int Age { get; set; }
//     public double Weight { get; set; }
//     public double Height { get; set; }
//     public double Temperature { get; set; }
//     public double CalculateBMI()
//     {
//         return Weight / (Height * Height);
//     }

// }
// class Validator
// {
//     public static int ValidateAge()
//     {
//         int age;
//         while (!int.TryParse(Console.ReadLine(), out age) || age <= 0 || age > 150)
//         {
//             System.Console.WriteLine("Enter valid age between 0 to 150");
//         }
//         return age;
//     }
//     public static double ValidateWeight()
//     {
//         double weight;
//         while(!double.TryParse(Console.ReadLine(), out weight) || weight <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-zero weight");
//         }
//         return weight;
//     }
//     public static double ValidateHeight()
//     {
//         double height;
//         while(!double.TryParse(Console.ReadLine(), out height) || height <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-zero height");
//         }
//         return height;
//     }
//     public static double ValidateTemperature()
//     {
//         double temperature;
//         while(!double.TryParse(Console.ReadLine(), out temperature) || temperature < 30 || temperature > 45)
//         {
//             System.Console.WriteLine("Enter valid temperature between 30 to 45 in Celsius");
//         }
//         return temperature;
//     }

// }
// class Program
// {
//     public static void Main()
//     {
//         Patient patient = new Patient();
//         System.Console.Write("Enter Age = ");
//         patient.Age = Validator.ValidateAge();
//         System.Console.Write("Enter Weight in kg = ");
//         patient.Weight = Validator.ValidateWeight();
//         System.Console.Write("Enter Height in m = ");
//         patient.Height = Validator.ValidateHeight();
//         System.Console.Write("Enter Temperature in Celsius = ");
//         patient.Temperature = Validator.ValidateTemperature();
//         System.Console.WriteLine("Patient Summary:");
//         System.Console.WriteLine($"Age: {patient.Age}");
//         System.Console.WriteLine($"Weight: {patient.Weight}");
//         System.Console.WriteLine($"Height: {patient.Height}");
//         System.Console.WriteLine($"Temperature: {patient.Temperature}");
//         System.Console.WriteLine($"BMI: {patient.CalculateBMI():F2}");

//     }
// }


// Ques 10
// H5
// A financial analysis tool receives investment details entered manually. Different investment types apply different return calculations, and users often enter invalid percentages or investment amounts.
// Develop a Console application with an extensible class design where different investment calculators implement a common interface. Accept validated numeric inputs, calculate projected returns, handle conversion errors, round results appropriately, and ensure the application is easy to extend for future investment types.
// Interfaces, multiple classes, arithmetic operations, Math functions, Parse/TryParse, type conversions, extensibility, validation
// Investment Type, Principal Amount, Annual Rate (%), Duration (Years)
// Display projected investment value with proper rounding. Invalid numeric input, negative values, or impossible percentages must be handled safely without crashing.

interface IInvesmentCalculator
{
    double InvestmentReturn(double principle, double rate, double time);
}
class CompoundInvestment : IInvesmentCalculator
{
    public double InvestmentReturn(double principle, double rate, double time)
    {
        return principle * Math.Pow((1 + rate / 100), time);
    }
}
class SimpleInvestment : IInvesmentCalculator
{
    public double InvestmentReturn(double principle, double rate, double time)
    {
        return principle * (1 + (rate / 100) * time);
    }
}
class Program
{
    public static void Main()
    {
        System.Console.Write("Enter Investment Type (Simple/Compound): ");
        string investmentType = Console.ReadLine().Trim().ToLower();

        double principle;
        System.Console.Write("Enter Principal Amount: ");
        while (!double.TryParse(Console.ReadLine(), out principle) || principle <= 0)
        {
            System.Console.WriteLine("Enter valid non-zero principal amount");
        }

        double rate;
        System.Console.Write("Enter Annual Rate (%): ");
        while (!double.TryParse(Console.ReadLine(), out rate) || rate < 0 || rate > 100)
        {
            System.Console.WriteLine("Enter valid annual rate between 0 to 100");
        }

        double time;
        System.Console.Write("Enter Duration (Years): ");
        while (!double.TryParse(Console.ReadLine(), out time) || time <= 0)
        {
            System.Console.WriteLine("Enter valid non-zero duration in years");
        }
        IInvesmentCalculator calculator;
        if (investmentType == "simple")
        {
            calculator = new SimpleInvestment();
        }
        else if (investmentType == "compound")
        {
            calculator = new CompoundInvestment();
        }
        else
        {
            System.Console.WriteLine("Invalid investment type");
            return; 

        }
        double projectedValue = calculator.InvestmentReturn(principle, rate, time);
        System.Console.WriteLine($"Projected Investment Value = {Math.Round(projectedValue, 2):F2}");   
    

       
    }
}