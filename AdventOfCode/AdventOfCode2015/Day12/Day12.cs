using CsharpDotNetUtils;

namespace AdventOfCode2015.Day12;

using System.Text.Json;

public class Day12
{
    public static void PartOne()
    {
        var jsonInput = File.ReadAllText(@"Day12/input.json");
        var numbers = TextUtils.GetIntegersWithSignFromString(jsonInput);
        var sum = 0;
        
        numbers.ForEach(n => sum += n);
        Console.WriteLine($"[Day12/Part1]: Sum = {sum}");
    }
}