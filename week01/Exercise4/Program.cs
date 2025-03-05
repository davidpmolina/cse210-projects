using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Step 1: Create a list to store the numbers
        List<int> numbers = new List<int>();

        // Ask the user for numbers
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int input;

        // Loop to get numbers from the user
        do
        {
            Console.Write("Enter number: ");
            input = int.Parse(Console.ReadLine());

            if (input != 0) // Only add non-zero numbers to the list
            {
                numbers.Add(input);
            }

        } while (input != 0); // Stop when the user enters 0

        // Step 2: Compute the sum, average, and maximum number

        if (numbers.Count > 0) // Ensure that the list is not empty
        {
            // Compute the sum
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }

            // Compute the average
            double average = sum / (double)numbers.Count;

            // Find the largest number
            int maxNumber = numbers[0];
            foreach (int number in numbers)
            {
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            // Output the results
            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {maxNumber}");
        }
        else
        {
            // If the list is empty (i.e., user entered 0 right away), display a message
            Console.WriteLine("No numbers were entered.");
        }
    }
}
