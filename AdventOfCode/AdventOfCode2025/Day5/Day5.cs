using CsharpDotNetUtils;

namespace AdventOfCode2025.Day5;

public class Day5
{
    public static void Part1()
    {
        var ranges = new List<string>();
        var ids = new List<string>();
        long freshIdCount = 0;
        
        using (var sr = TextUtils.GetStreamReaderFromTextFile(@"Day5/input.txt"))
        {
            var line = string.Empty;
            while (true)
            {
                line = sr.ReadLine();
                if (line == "\n" || line == "")
                {
                    break;
                }
                ranges.Add(line);
            }

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                ids.Add(line);
            }
            
            foreach (var id in ids)
            {
                var idNumber = long.Parse(id);
                foreach (var range in ranges)
                {
                    var rangeMin = long.Parse(range.Split('-')[0]);
                    var rangeMax = long.Parse(range.Split('-')[1]);
                    if (idNumber >= rangeMin && idNumber <= rangeMax)
                    {
                        freshIdCount++;
                        break;
                    }
                }
            }
        }
        Console.WriteLine($"[Day 5 | Part 1]: {freshIdCount}");
    }
}