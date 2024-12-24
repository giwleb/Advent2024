var guard = (1, 1);
var guarddirection = 'X';
var mapPath = "..\\..\\..\\map.txt";
var map = new List<string>();
var numLoops = 0;
foreach (var line in File.ReadLines(mapPath))
{
    map.Add(line);
}
// find the width of the map
var width = map[0].Length;
var height = map.Count;

// find the guard (either <, >, ^, or v) in the map as a tuple of (x, y)
for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var c = map[y][x];
        if (c == '<' || c == '>' || c == '^' || c == 'v')
        {
            guard = (x, y);
            //switch the guard position to a period
            map[y] = map[y].Remove(x, 1).Insert(x, ".");
            guarddirection = c;
            break;
        }
    }
}

// loop through the entire map x and y, except for the guard position and any non-. characters
for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        if (x == guard.Item1 && y == guard.Item2)
        {
            continue;
        }
        if (map[y][x] != '.')
        {
            continue;
        }
        Console.WriteLine($"Putting an obstacle at {x}, {y}");
        map[y] = map[y].Remove(x, 1).Insert(x, "X");
        if (Findloop(guard, guarddirection, map))
        {
            numLoops++;
            Console.WriteLine($"Loop Detected. Loop # {numLoops}");

        }
        // put the period back
        map[y] = map[y].Remove(x, 1).Insert(x, ".");
    }
}
Console.WriteLine($"Total number of loops detected: {numLoops}");
return 0;

bool Findloop((int, int) g, char gdir, List<string> map)
{
    char gd = gdir;
    // create a list of visited collision positions
    Dictionary<(int, int), int> visited = [];
    // keep track of last poisition in case no movement happens
    var lastpos = (-1, -1);
    var moveCount = 0;
    while (true)
    {
        var (i, j) = g;
        var (newi, newj) = g;
        switch (gd)
        {
            case '>':
                newi++;
                break;
            case '<':
                newi--;
                break;
            case '^':
                newj--;
                break;
            case 'v':
                newj++;
                break;
        }

        if (newi < 0 || newi >= width || newj < 0 || newj >= height)
        {
            Console.WriteLine($"Guard is out of bounds! Last postion was {i}, {j}");
            return false;
        }
        // Collision Check
        if (map[newj][newi] == '.')
        {
            // move the guard
            g = (newi, newj);
            //Console.WriteLine($"New postion: {g}.");
        }
        else
        {
            //Console.WriteLine("Collision detected!");
            // increment the number of times we've seen this position in the dictionary unless the position is the same as the last position
            if (g != lastpos && visited.TryGetValue(g, out int cnt))
            {
                visited[g] = cnt + 1;
            }
            else
            {
                visited[g] = 1;
            }

            moveCount++;
            
            if (visited[g] > 1)
            {
                Console.WriteLine($"Loop detected.");
                return true;
            }

            lastpos = g;

            // turn 90 degrees to the right
            switch (gd)
            {
                case '>':
                    gd = 'v';
                    break;
                case '<':
                    gd = '^';
                    break;
                case '^':
                    gd = '>';
                    break;
                case 'v':
                    gd = '<';
                    break;
            }
        }
    }
}