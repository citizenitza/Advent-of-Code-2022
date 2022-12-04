// See https://aka.ms/new-console-template for more information
using Day_2;

Console.WriteLine("Hello, World!");
Input input = new Input();

Console.WriteLine("Score of all rounds: " + input.GetScore().ToString());
Console.WriteLine("Score of all rounds corrrected: " + input.GetScore_2().ToString());
