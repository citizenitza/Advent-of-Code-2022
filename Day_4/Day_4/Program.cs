// See https://aka.ms/new-console-template for more information
using Day_4;
Console.WriteLine("Hello, World!");
Input input = new Input();

Console.WriteLine("Overlap count: " + input.CalculateOverlappings());
Console.WriteLine("Partial Overlap count: " + input.CalculatePartialOverlappings());
