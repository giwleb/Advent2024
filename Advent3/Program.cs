using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        var total = 0;
        var filePath = "..\\..\\..\\input.txt";
        var line = File.ReadAllText(filePath);
        line = line.Replace("\r\n", "");
        var mulOperations = new Regex(@"mul\((?<num1>\d+),(?<num2>\d+)\)");

        // loop through all the matches, treating them as a Match object
        foreach (Match match in mulOperations.Matches(line))
        {
            var num1 = int.Parse(match.Groups["num1"].Value);
            var num2 = int.Parse(match.Groups["num2"].Value);
            Console.WriteLine($"{num1} * {num2} = {num1 * num2}");
            total += num1 * num2;
            Console.WriteLine($"Total: {total}");
        }
    }
}