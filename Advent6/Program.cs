var guard = (1, 1);
var guarddirection = '>';
var mapPath = "..\\..\\..\\map.txt";
// read the map file
var map = new List<string>();
foreach (var line in File.ReadLines(mapPath))
{
    map.Add(line);
}
// find the width of the map
var width = map[0].Length;
// find the height of the map
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

Console.WriteLine($"Guard is at {guard} facing {guarddirection}");
var distinctPositionsVisited = new List<(int, int)> {guard};
var positionsVisited = 1;
while (true)
{
    var (x, y) = guard;
    var (newx, newy) = guard;
    switch (guarddirection)
    {
        case '>':
            newx++;
            break;
        case '<':
            newx--;
            break;
        case '^':
            newy--;
            break;
        case 'v':
            newy++;
            break;
    }

    if (newx < 0 || newx >= width || newy < 0 || newy >= height)
    {
        Console.WriteLine($"Guard is out of bounds! Last postion was {x}, {y}");
        break;
    }
    Console.WriteLine($"Guard is moving to {newx}, {newy}");
    // Collision Check
    if (map[newy][newx] == '.')
    {
        // move the guard
        guard = (newx, newy);
        // add the position to the list of visited positions if it's not already there
        positionsVisited++;
        if (!distinctPositionsVisited.Contains(guard))
        {
            distinctPositionsVisited.Add(guard);            
        }
        Console.WriteLine($"New postion: {guard}. Positions visited: {positionsVisited}. Distinct positions visited count: {distinctPositionsVisited.Count}");
    }
    else
    {
        Console.WriteLine("Collision detected!");
        // turn 90 degrees to the right
        switch (guarddirection)
        {
            case '>':
                guarddirection = 'v';
                break;
            case '<':
                guarddirection = '^';
                break;
            case '^':
                guarddirection = '>';
                break;
            case 'v':
                guarddirection = '<';
                break;
        }
    }
    Console.WriteLine($"Guard is now at {guard} facing {guarddirection}");
}
Console.WriteLine($"Positions visisted: {positionsVisited}. Distinct Positions visited: {distinctPositionsVisited.Count}");
