using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        var total = 0;
        var filePath = "..\\..\\..\\input.txt";
        var line = File.ReadAllText(filePath);
        line = line.Replace("\r\n", "");

        // remove anything between don't() and do() or the end of the line
        var dontDoOperations = new Regex(@"don't\(\).*?(?:do\(\)|$)");
        line = dontDoOperations.Replace(line, "");

        var mulOperations = new Regex(@"mul\((?<num1>\d+),(?<num2>\d+)\)");
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