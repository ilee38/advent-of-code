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

    public static void PartTwo()
    {
        var sum = 0;
        
        using (var jsonInput = JsonDocument.Parse(File.ReadAllText(@"Day12/input.json")))
        { 
              var root = jsonInput.RootElement;
              sum += Visit(root);
        }
        Console.WriteLine($"[Day12/PartTwo]: Sum = {sum}");
    }

    private static int Visit(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Number)
        {
            return element.GetInt32();
        }
        if (element.ValueKind == JsonValueKind.Array)
        {
            var sum = 0;
            foreach (var child in element.EnumerateArray())
            {
                sum += Visit(child);
            }
            return sum;
        }
        if (element.ValueKind == JsonValueKind.Object)
        {
            var sum = 0;
            var dict = element.EnumerateObject().ToDictionary(p => p.Name, p => p.Value);
            var red = dict.Values.Where(v => v.ValueKind == JsonValueKind.String && v.GetString().ToLowerInvariant() == "red").Any();
            if (red)
            {
                return 0;
            }
            else
            {
                foreach (var child in element.EnumerateObject())
                {
                    sum += Visit(child.Value);
                }
                return sum;
            }
        }
        return 0;
    }
}