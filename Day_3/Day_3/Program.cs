// See https://aka.ms/new-console-template for more information
using Day_3;
Console.WriteLine("Hello, World!");
Input input = new Input();

Console.WriteLine("Sum of priorites! " + input.CalcPrioritySum());
Console.WriteLine("Sum of group badge priorites! " + input.CalcBadgePriority());
