using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3;

public class Day3
{
    private readonly Regex mulPattern = new Regex(@"mul\(\d+\,\d+\)");
    private readonly Regex mulAndConditionalsPattern = new Regex(@"mul\(\d+\,\d+\)|do\(\)|don\'t\(\)");
    private readonly Regex digitsPattern = new Regex(@"\d+");

    private double runningTotal = 0;

    public void PartOneUncorruptedMul()
    {
        var memoryContent = File.ReadAllText(@"Day3/day3-input.txt");
        var mulOperationMatches = mulPattern.Matches(memoryContent);
        foreach (Match mul in mulOperationMatches)
        {
            var mulOp = mul.Value;
            var digitsToMultiply = digitsPattern.Matches(mulOp);
            runningTotal += (int.Parse(digitsToMultiply[0].Value) * int.Parse(digitsToMultiply[1].Value));
        }
        Console.WriteLine($"Day 3 / part 1 mul totals: {runningTotal}");
    }

    public void PartTwoConditionedMul()
    {
        var memoryContent = File.ReadAllText(@"Day3/day3-input.txt");
        var allInstructions = mulAndConditionalsPattern.Matches(memoryContent);
        var ignore = false;
        foreach (Match instruction in allInstructions)
        {
            var operation = instruction.Value;
            switch (operation)
            {
               case "don't()":
                   ignore = true;
                   break;
               case "do()":
                   ignore = false;
                   break;
               default:
                   if (!ignore)
                   {
                       var digitsToMultiply = digitsPattern.Matches(operation);
                       runningTotal += (int.Parse(digitsToMultiply[0].Value) * int.Parse(digitsToMultiply[1].Value));
                   }
                   break;
            }
        }
        Console.WriteLine($"Day 3 / part 2 mul total: {runningTotal}");
    }
}
