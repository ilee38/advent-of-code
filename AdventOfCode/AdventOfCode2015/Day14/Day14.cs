using CsharpDotNetUtils;

namespace AdventOfCode2015.Day14;

public class Day14
{
    private const int TOTAL_TIME_GIVEN = 2503;
    
    public static void PartOne()
    {
        var bestDistance = int.MinValue;
        
        using (var reindeersStats = TextUtils.GetStreamReaderFromTextFile(@"Day14/input.txt"))
        {
            while (!reindeersStats.EndOfStream)
            {
                var line = reindeersStats.ReadLine().TrimEnd('.').Split(' ');
                var speed = int.Parse(line[3]); 
                var travelTime = int.Parse(line[6]);
                var restTime = int.Parse(line[13]);
                
                var cycleTime = travelTime + restTime;
                var distancePerCycle = speed * travelTime;
                var numberOfCycles = TOTAL_TIME_GIVEN / cycleTime;
                
                var distanceInNumberOfCycles = distancePerCycle * numberOfCycles;
                var timeElapsed = cycleTime * numberOfCycles;
                var timeRemaining = TOTAL_TIME_GIVEN - timeElapsed;

                var totalDistance = timeRemaining >= travelTime
                    ? distanceInNumberOfCycles + distancePerCycle
                    : distanceInNumberOfCycles + (speed * timeRemaining);
                
                if (totalDistance > bestDistance)
                {
                    bestDistance = totalDistance;
                }
            }
        }
        Console.WriteLine($"Day 14 part one result: {bestDistance}");
    }
}