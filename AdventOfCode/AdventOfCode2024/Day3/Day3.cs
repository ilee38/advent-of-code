using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3;

public class Day3
{
    private readonly Regex mulPattern = new Regex(@"mul\(\d+\,\d+\)");
    private readonly Regex digits = new Regex(@"\d+");

    private double runningTotal = 0;

    public void PartOneUncorruptedMul()
    {
        var memoryContent = File.ReadAllText(@"Day3/day3-input.txt");
        var mulOperationMatches = mulPattern.Matches(memoryContent);
        foreach (Match mul in mulOperationMatches)
        {
            var mulOp = mul.Value;
            var digitsToMultiply = digits.Matches(mulOp);
            runningTotal += (int.Parse(digitsToMultiply[0].Value) * int.Parse(digitsToMultiply[1].Value));
        }
        Console.WriteLine($"Day 3 / part 1 mul totals: {runningTotal}");
    }
}
