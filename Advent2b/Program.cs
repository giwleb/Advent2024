public class Program
{
    public static void Main()
    {
        var filePath = "..\\..\\..\\input.txt";
        int totalSafe = 0;

        foreach (var line in File.ReadLines(filePath))
        {
            var levels = line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            Console.WriteLine(line);
            var isSafe = IsSafe(levels);
            if (!isSafe)
            {                
                for (int i = 0; i < levels.Count; i++)
                {
                    var newLevels = new List<int>(levels);
                    newLevels.RemoveAt(i);
                    if (IsSafe(newLevels))
                    {
                        isSafe = true;
                        break;
                    }
                }
                if (isSafe)
                {
                    Console.WriteLine("SAFE");
                    totalSafe++;
                }
            }
            else
            {
                Console.WriteLine("SAFE");
                totalSafe++;
            }
        }

        Console.WriteLine($"The answer is {totalSafe}");
    }

    public static bool IsSafe(List<int> levels)
    {
        var up = levels[0] < levels[1];
        for (int i = 0; i < levels.Count - 1; i++)
        {
            int x = levels[i];
            var y = levels[i + 1];

            if (x == y || up != x < y || Math.Abs(x - y) > 3)
            {
                return false;
            }
        }
        return true;
    }
}