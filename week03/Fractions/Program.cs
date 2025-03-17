using System;

public class Program
{
    public static void Main(string[] args)
    {
        
        Fraction fraction1 = new Fraction();
        Fraction fraction2 = new Fraction(6);
        Fraction fraction3 = new Fraction(6, 7);

        
        fraction1.SetTop(1);
        fraction1.SetBottom(1);

        fraction2.SetTop(5);
        fraction2.SetBottom(1);

        fraction3.SetTop(3);
        fraction3.SetBottom(4);

        Fraction fraction4 = new Fraction(1,3);

        
        Console.WriteLine(fraction1.GetFractionString());
        Console.WriteLine(fraction1.GetDecimalValue());
        Console.WriteLine(fraction2.GetFractionString());
        Console.WriteLine(fraction2.GetDecimalValue());
        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());
        Console.WriteLine(fraction4.GetFractionString());
        Console.WriteLine(fraction4.GetDecimalValue());
    }
}