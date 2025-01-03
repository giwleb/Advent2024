﻿using System.Collections.Generic;

var rulesPath = "..\\..\\..\\rules.txt";
var pagesPath = "..\\..\\..\\pages.txt";

var rules = new List<string>();
foreach (var line in File.ReadLines(rulesPath))
{
    rules.Add(line);
}

var beforerules = new Dictionary<int, List<int>>();
var afterrules = new Dictionary<int, List<int>>();
var middles = new List<int>();
foreach (var line in File.ReadLines(rulesPath))
{
    var parts = line.Split('|');
    var before = int.Parse(parts[0]);
    var after = int.Parse(parts[1]);
    if (!beforerules.TryGetValue(before, out List<int>? beforevalues))
    {
        beforevalues = [];
        beforerules[before] = beforevalues;
    }
    beforevalues.Add(after);
    if (!afterrules.TryGetValue(after, out List<int>? aftervalues))
    {
        aftervalues = [];
        afterrules[after] = aftervalues;
    }
    aftervalues.Add(before);
}

foreach (var line in File.ReadLines(pagesPath))
{
    Console.WriteLine($"Checking {line}");
    var good = true;
    var pages = line.Split(',');

    for (var i = 0; i < pages.Length; i++)
    {
        var page = int.Parse(pages[i]);
        var next = i + 1 < pages.Length ? int.Parse(pages[i + 1]) : -1;
        var prev = i - 1 >= 0 ? int.Parse(pages[i - 1]) : -1;
        var mustbeafters = beforerules.GetValueOrDefault(page, []);
        var mustbebefores = afterrules.GetValueOrDefault(page, []);
        // check if the next page is not in the "mustbeafters" rules
        if (next != -1 && !mustbeafters.Contains(next))
        {
            Console.WriteLine($"Oops Page {page} can't be before {next}");
            good = false;
            break;
        }
        // check if the previous page is not in the "mustbebefores" rules
        if (prev != -1 && !mustbebefores.Contains(prev))
        {
            good = false;
            Console.WriteLine($"Oops Page {page} can't be after page {prev}");
        }
    }
    if (good)
    {
        Console.WriteLine("Good.. skipping");        
    }
    else
    {
        Console.WriteLine("Bad");
        SortIt(pages);
        Console.WriteLine($"New Order: {string.Join(",", pages)}");

        var middle = pages[pages.Length / 2];
        middles.Add(int.Parse(middle));
    }
    
    var sum = 0;
    foreach (var m in middles)
    {
        sum += m;
    }
    Console.WriteLine($"Sum of middles of resorted rows: {sum}");
}

void SortIt(string[] pages)
{
    bool swapped = false;
    for (int i = 0; i < pages.Length - 1; i++)
    {
        var comp = pages[i] + "|" + pages[i + 1];
        if (!rules.Contains(comp))
        {
            comp = pages[i + 1] + "|" + pages[i];
            if (rules.Contains(comp))
            {
                swapped = true;
                (pages[i + 1], pages[i]) = (pages[i], pages[i + 1]);
            }
        }
    }
    if (swapped)
    {
        SortIt(pages);
    }

    return;
}