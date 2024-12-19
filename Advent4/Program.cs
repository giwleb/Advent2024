using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        var hftotal = 0;
        var hbtotal = 0;
        var vdtotal = 0;
        var vutotal = 0;
        var dultotal = 0;
        var durtotal = 0;
        var ddltotal = 0;
        var ddrtotal = 0;
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
                // if the character is not 'X' move on
                if (line[x] != 'X')
                {
                    continue;
                }

                // horizontal forwards
                if (x + 3 < line.Length && line[x + 1] == 'M' && line[x + 2] == 'A' && line[x + 3] == 'S')
                {
                    hftotal++;
                }

                // horizontal backwards
                if (x - 3 >= 0 && line[x - 1] == 'M' && line[x - 2] == 'A' && line[x - 3] == 'S')
                {
                    hbtotal++;
                }
                //vertical downwards
                if (y + 3 < list.Count && list[y + 1][x] == 'M' && list[y + 2][x] == 'A' && list[y + 3][x] == 'S')
                {
                    vdtotal++;
                }

                // vertical upwards
                if (y - 3 >= 0 && list[y - 1][x] == 'M' && list[y - 2][x] == 'A' && list[y - 3][x] == 'S')
                {
                    vutotal++;
                }

                // diagonal up left
                if (x - 3 >= 0 && y - 3 >= 0 && list[y - 1][x - 1] == 'M' && list[y - 2][x - 2] == 'A' && list[y - 3][x - 3] == 'S')
                {
                    dultotal++;
                }

                // diagonal up right
                if (x + 3 < line.Length && y - 3 >= 0 && list[y - 1][x + 1] == 'M' && list[y - 2][x + 2] == 'A' && list[y - 3][x + 3] == 'S')
                {
                    durtotal++;
                }

                // diagonal down left
                if (x - 3 >= 0 && y + 3 < list.Count && list[y + 1][x - 1] == 'M' && list[y + 2][x - 2] == 'A' && list[y + 3][x - 3] == 'S')
                {
                    ddltotal++;
                }

                // diagonal down right
                if (x + 3 < line.Length && y + 3 < list.Count && list[y + 1][x + 1] == 'M' && list[y + 2][x + 2] == 'A' && list[y + 3][x + 3] == 'S')
                {
                    ddrtotal++;
                }
            }
        }

        Console.WriteLine($"Total horizontal forwards is {hftotal}");
        Console.WriteLine($"Total horizontal backwards is {hbtotal}");
        Console.WriteLine($"Total vertical down is {vdtotal}");
        Console.WriteLine($"Total vertical up is {vutotal}");
        Console.WriteLine($"Total diagonal up left is {dultotal}");
        Console.WriteLine($"Total diagonal up right is {durtotal}");
        Console.WriteLine($"Total diagonal down left is {ddltotal}");
        Console.WriteLine($"Total diagonal down right is {ddrtotal}");
        Console.WriteLine($"Total is {hftotal + hbtotal + vdtotal + vutotal + dultotal + durtotal + ddltotal + ddrtotal}");
    }
}