public class Program
{
    public static void Main()
    {
        var xtotal = 0;
        var filePath = "..\\..\\..\\input.txt";
        var list = new List<string>();
        foreach (var line in File.ReadLines(filePath))
        {
            list.Add(line);
        }

        for (int y = 0; y < list.Count; y++)
        {
            var line = list[y];
            for (int x = 0; x < line.Length; x++)
            {
                // if the character is not 'A' move on
                if (line[x] != 'A')
                {
                    continue;
                }

                // M M  
                //  A
                // S S
                if (x - 1 >= 0 && y - 1 >= 0 && x + 1 < line.Length && y + 1 < list.Count && list[y - 1][x - 1] == 'M' && list[y + 1][x + 1] == 'S')
                {
                    if (x + 1 < line.Length && y - 1 >= 0 && x - 1 >= 0 && y + 1 < list.Count && list[y - 1][x + 1] == 'M' && list[y + 1][x - 1] == 'S')
                    {
                        xtotal++;
                    }
                }

                // S M  
                //  A
                // S M
                if (x - 1 >= 0 && y - 1 >= 0 && x + 1 < line.Length && y + 1 < list.Count && list[y - 1][x - 1] == 'S' && list[y + 1][x + 1] == 'M')
                {
                    if (x + 1 < line.Length && y - 1 >= 0 && x - 1 >= 0 && y + 1 < list.Count && list[y - 1][x + 1] == 'M' && list[y + 1][x - 1] == 'S')
                    {
                        xtotal++;
                    }
                }

                // M S  
                //  A
                // M S
                if (x - 1 >= 0 && y - 1 >= 0 && x + 1 < line.Length && y + 1 < list.Count && list[y - 1][x - 1] == 'M' && list[y + 1][x + 1] == 'S')
                {
                    if (x + 1 < line.Length && y - 1 >= 0 && x - 1 >= 0 && y + 1 < list.Count && list[y - 1][x + 1] == 'S' && list[y + 1][x - 1] == 'M')
                    {
                        xtotal++;
                    }
                }

                // S S  
                //  A
                // M M
                if (x - 1 >= 0 && y - 1 >= 0 && x + 1 < line.Length && y + 1 < list.Count && list[y - 1][x - 1] == 'S' && list[y + 1][x + 1] == 'M')
                {
                    if (x + 1 < line.Length && y - 1 >= 0 && x - 1 >= 0 && y + 1 < list.Count && list[y - 1][x + 1] == 'S' && list[y + 1][x - 1] == 'M')
                    {
                        xtotal++;
                    }
                }
            }
        }

        Console.WriteLine($"Total is {xtotal}");
    }
}