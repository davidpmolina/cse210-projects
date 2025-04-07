using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // Step 5: Test the Square class
        Square square = new Square("Blue", 5);
        Console.WriteLine($"Square Color: {square.Color}, Area: {square.GetArea()}");

        // Step 6: Test the Rectangle class
        Rectangle rectangle = new Rectangle("Red", 4, 6);
        Console.WriteLine($"Rectangle Color: {rectangle.Color}, Area: {rectangle.GetArea()}");

        // Step 6: Test the Circle class
        Circle circle = new Circle("Green", 3);
        Console.WriteLine($"Circle Color: {circle.Color}, Area: {circle.GetArea()}");

        // Step 7: Build a List
        List<Shape> shapes = new List<Shape>();
        shapes.Add(square);
        shapes.Add(rectangle);
        shapes.Add(circle);

        // Step 7: Iterate through the list of shapes
        Console.WriteLine("\nAll Shapes:");
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.Color}, Area: {shape.GetArea()}");
        }
    }
}