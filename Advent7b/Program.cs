var filePath = "..\\..\\..\\input.txt";
var total = 0L;
foreach (var line in File.ReadLines(filePath))
{
    var columns = line.Split([':'], StringSplitOptions.RemoveEmptyEntries);
    var result = long.Parse(columns[0].Trim());
    var parts = columns[1].Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
    if (FindSolution(parts, result))
    {
        total += result;
    }
    Console.WriteLine($"Total is {total}");
    Console.WriteLine($"-----------------");
}

// Note this is ChatGPT's solution, not mine. Hoping to learn from it.
static bool CheckCombination(List<long> numbers, long target)
{
    if (numbers == null || numbers.Count < 2)
        throw new ArgumentException("The list must contain at least two integers.");

    return CheckCombinationRecursive(numbers, target, 0, numbers[0]);
}

static bool FindSolution(List<long> numbers, long target)
{
    if (numbers == null || numbers.Count < 2)
    {
        Console.WriteLine("No possible solution");
        return false;
    }
    Console.WriteLine($"Target: {target}");

    string solution = string.Empty;
    if (CheckCombinationRecursive2(numbers, target, 0, numbers[0], $"{numbers[0]}", out solution))
    {
        Console.WriteLine($"Solution: {solution}");
        return true;
    }
    else
    {
        Console.WriteLine("No possible solution");
        return false;
    }
}

static bool CheckCombinationRecursive(List<long> numbers, long target, int index, long current)
{
    // The base case is a condition that stops the recursion.
    // The base case here is when we reach the last number in the list.
    // At this point, we simply check if the current value equals the target.
    // It doesn't matter how we got here (plus, multiply), we just need to check the result.
    if (index == numbers.Count - 1)
        return current == target;

    // The recursive case defines how the problem is broken down into smaller parts and solved incrementally
    // We maintain the total thus far using the variable `current`. The smaller parts is to either add or multiply the next number to current
    // and to increment the index to move to the next number in the list.
    var next = numbers[index + 1];

    return CheckCombinationRecursive(numbers, target, index + 1, current + next) ||
            CheckCombinationRecursive(numbers, target, index + 1, current * next);
}

static bool CheckCombinationRecursive2(List<long> numbers, long target, int index, long current, string path, out string solution)
{
    // Base case: if we reach the last number
    if (index == numbers.Count - 1)
    {
        if (current == target)
        {
            solution = path;
            return true;
        }
        solution = string.Empty;
        return false;
    }

    // Recursive case: Add or multiply the next number
    long next = numbers[index + 1];

    // Try addition
    if (CheckCombinationRecursive2(numbers, target, index + 1, current + next, $"{path} + {next}", out solution))
        return true;

    // Try multiplication
    if (CheckCombinationRecursive2(numbers, target, index + 1, current * next, $"{path} * {next}", out solution))
        return true;

    // Need to multiple current by 10^next.Length and add next to it. A reminder that ^ is not the power operator in C#.
    long concatenated = current * (long)Math.Pow(10, next.ToString().Length) + next;
    if (CheckCombinationRecursive2(numbers, target, index + 1, concatenated, $"{path} || {next}", out solution))
        return true;

    solution = string.Empty;
    return false;
}
