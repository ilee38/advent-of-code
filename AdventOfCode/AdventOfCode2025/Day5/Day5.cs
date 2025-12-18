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

    /// <summary>
    /// Still returns the incorrect answer... need to debug
    /// </summary>
    public static void Part2()
    {
        var ranges = new List<Tuple<long, long>>();
        long freshCount = 0;
        
        using (var sr = TextUtils.GetStreamReaderFromTextFile(@"Day5/testinput.txt"))
        {
            var line = string.Empty;
            while (true)
            {
                line = sr.ReadLine();
                if (line == "\n" || line == "")
                {
                    break;
                }

                var range = new Tuple<long, long>(long.Parse(line.Split('-')[0]), long.Parse(line.Split('-')[1]));
                ranges.Add(range);
            }
        }
        
        ranges.Sort();
        var q = new Queue<Tuple<long, long>>();
        foreach (var range in ranges)
        {
            q.Enqueue(range);
        }

        var currentRange = q.Dequeue();
        var currentRangeMin = currentRange.Item1;
        var currentRangeMax = currentRange.Item2;
        
        while (q.Count >= 0)
        {
            if (q.Count == 0)
            {
                freshCount += (currentRangeMax - currentRangeMin) + 1;
                break;
            }
            var nextRange = q.Peek();
            var nextRangeMin = nextRange.Item1;
            var nextRangeMax = nextRange.Item2;
            
            if (currentRangeMin >= nextRangeMin && currentRangeMax <= nextRangeMax) // Fully contained within
            {
                currentRangeMin = nextRangeMin;
                currentRangeMax = nextRangeMax;
                var _ =  q.Dequeue();
            }
            else if ((currentRangeMin >= nextRangeMin && currentRangeMin <= nextRangeMax) ||    // Overlap
                (currentRangeMax >= nextRangeMin && currentRangeMax <= nextRangeMax))
            {
                currentRangeMin =  Math.Min(currentRangeMin, nextRangeMin);
                currentRangeMax =  Math.Max(currentRangeMax, nextRangeMax);
                var _ =  q.Dequeue();
            }
            else
            {
                freshCount += (currentRangeMax - currentRangeMin) + 1;
                currentRange =  q.Dequeue();
                currentRangeMin = currentRange.Item1;
                currentRangeMax = currentRange.Item2;
            }
        }
        
        Console.WriteLine($"[Day 5 | Part 2]: {freshCount}");
    }
}