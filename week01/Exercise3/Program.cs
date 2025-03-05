using System;

class Program
{
    static void Main(string[] args)
    {
        
        Random rand = new Random();
        int magicNumber = rand.Next(1, 101); 

        int guess;

        do
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

        } while (guess != magicNumber);
    }
}