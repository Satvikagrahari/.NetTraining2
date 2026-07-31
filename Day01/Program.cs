using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

// Ques 1 
// A retail store cashier enters the price of an item, quantity purchased, and discount percentage. Invalid values are sometimes entered due to typing mistakes.
// Develop a Console application that accepts item price, quantity, and discount percentage. Validate all inputs using TryParse, calculate subtotal, discount amount, and final payable amount. Reject negative values and prompt the user appropriately.
// Console input/output, variables, primitive types, arithmetic operators, Math.Round(), TryParse, validation
// Price: 249.99 Quantity: 3 Discount: 10
// Display subtotal, discount amount, final payable amount rounded to two decimal places. Display validation errors for invalid or negative input.

// class Program{
//     public static void Main(string[] args){
//         double price;
//         Console.WriteLine("Enter Price: ");
//         while(!double.TryParse(Console.ReadLine(), out price) || price < 0)
//         {
//             System.Console.WriteLine("Enter non-neg Price");
//         }
//         int quantity;
//         System.Console.WriteLine("Enter quantity");
//         while(!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
//         {
//             System.Console.WriteLine("Enter valid quantity");
//         }
//         double discount;
//         System.Console.WriteLine("Enter discount percentage");
//         while(!double.TryParse(Console.ReadLine(), out discount) || discount < 0)
//         {
//             System.Console.WriteLine("Enter valid discount"); 
//         }
//         double subtotal = price*quantity;
//         double discountAmt = subtotal*discount/100;
//         double finalAmt = subtotal-discountAmt;

//         System.Console.WriteLine($"Subtotal = {Math.Round(subtotal,2):F2}");
//         System.Console.WriteLine($"Discount Amount = {Math.Round(discountAmt,2):F2}");
//         System.Console.WriteLine($"Final Payable Amount = {Math.Round(finalAmt,2):F2}");
//     }
// }


// Ques 2
// A fitness trainer wants a console utility that calculates Body Mass Index (BMI) for clients, but users frequently enter height in different formats.
// Build a Console application that accepts weight (kg) and height (meters). Validate numeric input, calculate BMI, round to two decimals, and classify the user into BMI categories. 
// Handle invalid and zero values safely.
// Variables, primitive types, Math operators, Parse/TryParse, conditional logic, Math.Round()
// Weight: 72.5 Height: 1.72
// Display BMI and appropriate category. Reject zero or negative height and invalid numeric values.

// class Program
// {
//     public static void Main()
//     {
//         double weight;
//         double height;
//         System.Console.WriteLine("Enter weight in kg");
//         while (!double.TryParse(Console.ReadLine(), out weight) || weight <= 0)
//         {
//             System.Console.WriteLine("Enter non zero valid weight in kg");
//         }

//         System.Console.WriteLine("Enter height in meters");
//         while (!double.TryParse(Console.ReadLine(), out height) || height <= 0)
//         {
//             System.Console.WriteLine("Enter non zero valid height in meters");
//         }
//         double bmi = weight / (height * height);
//         System.Console.WriteLine($"BMI = {Math.Round(bmi, 2):F2}");
//         if (bmi < 18.5)
//         {
//             Console.WriteLine("Category: Underweight");
//         }
//         else if (bmi < 25)
//         {
//             Console.WriteLine("Category: Normal weight");
//         }
//         else if (bmi < 30)
//         {
//             Console.WriteLine("Category: Overweight");
//         }
//         else
//         {
//             Console.WriteLine("Category: Obese");
//         }
//     }
// }


// Ques 3
// A warehouse manager records package dimensions to calculate shipping volume. Employees occasionally enter decimal values incorrectly.
// Create a Console application that accepts length, width, and height. Validate inputs, calculate volume, and display the result. Reject invalid or non-positive dimensions and prevent calculation until valid data is entered.
// Console application, double, variables, operators, Parse/TryParse, validation
// Length: 25.5 Width: 18.2 Height: 12
// Display calculated volume. Handle invalid numeric entries gracefully without crashing.

// class Program
// {
//     public static void Main()
//     {
//         decimal length;
//         Console.WriteLine("Enter length: ");
//         while(!decimal.TryParse(Console.ReadLine(), out length) || length <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-neg length");
//         }

//         decimal width;
//         System.Console.WriteLine("Enter width");
//         while(!decimal.TryParse(Console.ReadLine(), out width) || width <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-neg width"); 
//         }

//         decimal height;
//         System.Console.WriteLine("Enter height");
//         while(!decimal.TryParse(Console.ReadLine(), out height) || height <= 0)
//         {
//             System.Console.WriteLine("Enter valid non-neg height");
//         }
        
//         decimal volume = length*width*height;
//         System.Console.WriteLine($"Volume = {Math.Round(volume,2):F2}");
//     }
// }


// Ques 4
// M4
// A bank employee manually enters customer deposit and withdrawal amounts to determine the remaining balance. Incorrect inputs are common.
// Design a Console application that accepts opening balance, total deposits, and total withdrawals. Validate all values and calculate the final balance. Prevent withdrawals from exceeding the available balance.
// Variables, arithmetic operators, numeric conversions, TryParse, validation, business rules
// Opening Balance: 5000 Deposits: 2500 Withdrawals: 3200
// Display updated balance. Show an error if withdrawals exceed available funds or inputs are invalid.


// class Program
// {
//     public static void Main()
//     {
//         double openingBal;
//         double deposits;
//         double withdrawals;

        
//         Console.Write("Enter Opening Balance: ");
//         while (!double.TryParse(Console.ReadLine(), out openingBal) || openingBal < 0)
//         {
//             Console.WriteLine("Invalid opening balance.");
//             Console.Write("Enter Opening Balance: ");
//         }

        
//         Console.Write("Enter Total Deposits: ");
//         while (!double.TryParse(Console.ReadLine(), out deposits) || deposits < 0)
//         {
//             Console.WriteLine("Invalid deposit amount.");
//             Console.Write("Enter Total Deposits: ");
//         }

//         Console.Write("Enter Total Withdrawals: ");
//         while (!double.TryParse(Console.ReadLine(), out withdrawals) || withdrawals < 0)
//         {
//             Console.WriteLine("Invalid withdrawal amount.");
//             Console.Write("Enter Total Withdrawals: ");
//         }

//         double availableBalance = openingBal + deposits;

//         if (withdrawals > availableBalance)
//         {
//             Console.WriteLine("Error: Withdrawal amount exceeds available balance.");
//             return;
//         }

//         double finalBalance = availableBalance - withdrawals;

//         Console.WriteLine($"Opening Balance : {openingBal:F2}");
//         Console.WriteLine($"Deposits        : {deposits:F2}");
//         Console.WriteLine($"Withdrawals     : {withdrawals:F2}");
//         Console.WriteLine($"Final Balance   : {finalBalance:F2}");
//     }
// }


// Ques 5
// M5
// A school administrator enters marks obtained in five subjects to calculate student performance. Some marks are accidentally entered as text or outside the valid range.
// Build a Console application that accepts marks for five subjects, validates that each mark is between 0 and 100, calculates total, average, percentage, and displays the rounded percentage.
// Arrays not required, variables, arithmetic operators, Parse/TryParse, Math.Round(), validation
// Five marks such as: 78, 82, 91, 65, 88
// Display total, average, percentage, and reject invalid marks or non-numeric values.

// int n = 5;
// double[] marks = new double[n];
// System.Console.WriteLine("Enter marks of 5 subject ");
// for(int i=0; i<n; i++)
// {
//     while(!double.TryParse(Console.ReadLine(), out marks[i]) || marks[i]<0 || marks[i] > 100)
//     {
//         System.Console.WriteLine("Enter valid marks between 0 to 100");
//     }

// }
// double total = 0;
// for(int i=0; i<n; i++)
// {
//     total += marks[i];
// }
// double average = total/n;
// double percentage = (total/(n*100))*100;

// System.Console.WriteLine($"Total = {total}");
// System.Console.WriteLine($"Average = {average}");
// System.Console.WriteLine($"Percentage = {Math.Round(percentage,2):F2}");