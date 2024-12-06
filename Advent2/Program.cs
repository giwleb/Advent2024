using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        var filePath = "..\\..\\..\\input.txt";
        List<int> firstColumn = [];
        List<int> secondColumn = [];
        int totalDiff = 0;
        ReadColumnsFromFile(filePath, firstColumn, secondColumn);

        foreach (var x in firstColumn)
        {
            //int y = secondColumn.Where((_) => _ == x).Count();
            //int z = x * y;
            //totalDiff += z;
            //Console.WriteLine(string.Join(", ", x, y, z, totalDiff));

            // terse
            totalDiff += x * secondColumn.Where((_) => _ == x).Count();
        }

        Console.WriteLine($"The answer is {totalDiff}");
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