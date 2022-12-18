// See https://aka.ms/new-console-template for more information

using Day_1_1;

Console.WriteLine("Hello, World!");

Input input = new Input();
Console.WriteLine("Max Calories count: " + input.MaxCalories().ToString());
Console.WriteLine("Max Calories Top3: " + input.MaxCaloriesTop3().ToString());