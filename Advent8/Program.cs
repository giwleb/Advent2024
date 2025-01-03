﻿// SAMPLE INPUT
//
//............
//........0...
//.....0......
//.......0....
//....0.......
//......A.....
//............
//............
//........A...
//.........A..
//............
//............

var map = new List<string>();
var mapPath = "..\\..\\..\\map.txt";
var antennasToExamine = new Dictionary<char, List<(int, int)>>();
var totalAntinodes = 0;
var antinodePoints = new HashSet<(int, int)>();
foreach (var line in File.ReadLines(mapPath))
{
    map.Add(line);
}

// step 1: scan the map looking for unique antennas, which are represented by anything other than a period
for (var y = 0; y < map.Count; y++)
{
    for (var x = 0; x < map[0].Length; x++)
    {
        if (map[y][x] != '.')
        {
            if (!antennasToExamine.TryGetValue(map[y][x], out List<(int, int)>? value))
            {
                value = [];
                antennasToExamine[map[y][x]] = value;
            }
            value.Add((x, y));
        }
    }
}

foreach (var antenna in antennasToExamine)
{
    Console.WriteLine($"Antenna {antenna.Key} is at:");
    foreach (var position in antenna.Value)
    {
        Console.WriteLine($"({position.Item1}, {position.Item2})");
    }
    Console.WriteLine();
}

// step 2: loop each pair of antennas of they same type
foreach (var antenna in antennasToExamine)
{
    if (antenna.Value.Count < 2)
    {
        continue;
    }
    for (var i = 0; i < antenna.Value.Count - 1; i++)
    {
        for (var j = i + 1; j < antenna.Value.Count; j++)
        {
            var antenna1 = antenna.Value[i];
            var antenna2 = antenna.Value[j];
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Checking {antenna.Key} at ({antenna1.Item1}, {antenna1.Item2}) and ({antenna2.Item1}, {antenna2.Item2})");

            // step 3: calculate a point on the line between the two antennas where that point is exactly twice as far away from the point as the other but still within the map
            var a1x = antenna1.Item1;
            var a1y = antenna1.Item2;
            var a2x = antenna2.Item1;
            var a2y = antenna2.Item2;
            var dx = a2x - a1x;
            var dy = a2y - a1y;
            var antinode1x = a1x + dx * 2;
            var antinode1y = a1y + dy * 2;
            var antinode2x = a2x + (-1 * dx) * 2;
            var antinode2y = (a2y + (-1 * dy) * 2);

            if (antinode1x < 0 || antinode1x >= map[0].Length || antinode1y < 0 || antinode1y >= map.Count)
            {
                Console.WriteLine($"antinode at ({antinode1x}, {antinode1y}) is out of bounds. Total antinodes: {totalAntinodes}");
            }
            else
            {
                totalAntinodes++;
                Console.WriteLine($"Found a point at ({antinode1x}, {antinode1y}). Total antinodes: {totalAntinodes}");
                antinodePoints.Add((antinode1x, antinode1y));
            }

            if (antinode2x < 0 || antinode2x >= map[0].Length || antinode2y < 0 || antinode2y >= map.Count)
            {
                Console.WriteLine($"antinode at ({antinode2x}, {antinode2y}) is out of bounds. Total antinodes: {totalAntinodes}");
                continue;
            }
            else
            {
                totalAntinodes++;
                Console.WriteLine($"Found a point at ({antinode2x}, {antinode2y}). Total antinodes: {totalAntinodes}");
                antinodePoints.Add((antinode2x, antinode2y));
            }
        }
    }
}

Console.WriteLine($"Total number of antinodes: {totalAntinodes}");
Console.WriteLine($"Unique antinode points: {antinodePoints.Count}");