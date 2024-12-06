using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        var filePath = "C:\\Users\\john.hutchinson\\source\\repos\\Advent2024\\Advent1\\input.txt"; 
        List<int> firstColumn = [];
        List<int> secondColumn = [];
        int totalDiff = 0;
        ReadColumnsFromFile(filePath, firstColumn, secondColumn);

        while (firstColumn.Count > 0 && secondColumn.Count > 0)
        {
            //var col1min = FindSmallestAndRemove(firstColumn);
            //var col2min = FindSmallestAndRemove(secondColumn);
            //var diff = Math.Abs(col1min - col2min);            
            //totalDiff += diff;
            //Console.WriteLine(string.Join(", ", col1min, col2min, diff, totalDiff));

            // terse
            totalDiff += Math.Abs(FindSmallestAndRemove(firstColumn) - FindSmallestAndRemove(secondColumn));
        }

        Console.WriteLine($"The answer is {totalDiff}");
    }

    public static int FindSmallestAndRemove(List<int> numbers)
    {
        int minValue = numbers[0];
        int idx = 0;

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] < minValue)
            {
                minValue = numbers[i];
                idx = i;
            }
        }

        numbers.RemoveAt(idx);
        return minValue;
    }

    public static void ReadColumnsFromFile(string filePath, List<int> firstColumn, List<int> secondColumn)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var columns = line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
            if (columns.Length >= 2)
            {
                if (int.TryParse(columns[0], out int firstValue) && int.TryParse(columns[1], out int secondValue))
                {
                    firstColumn.Add(firstValue);
                    secondColumn.Add(secondValue);
                }
                else
                {
                    throw new Exception("wtf?");
                }
            }
        }
    }
}