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

    public static void PartTwo()
    {
        var reindeerStats = GetReindeerStats(@"Day14/input.txt");
        var leadDistance = 0;
        var currentTime = 0;
        var leaders = new List<string>();

        while (currentTime < TOTAL_TIME_GIVEN)
        {
            currentTime += 1;
            foreach (var reindeer in reindeerStats.Keys)
            {
                if (CheckReindeerFlying(reindeerStats[reindeer], currentTime))
                {
                    reindeerStats[reindeer]["currentDistance"] += reindeerStats[reindeer]["speed"];
                }
            }

            foreach (var reindeer in reindeerStats.Keys)
            {
                var distanceSoFar = reindeerStats[reindeer]["currentDistance"];
                
                if (distanceSoFar > leadDistance)
                {
                    leadDistance = distanceSoFar;
                    leaders.Clear();
                    leaders.Add(reindeer);
                    continue;
                }
                if (distanceSoFar == leadDistance)
                {
                    if (!leaders.Contains(reindeer))
                    {
                        leaders.Add(reindeer);
                    }
                }
            }
            leaders.ForEach(x => reindeerStats[x]["points"] += 1);
        }

        var highestPoints = reindeerStats.Values.Select(reindeer => reindeer["points"]).Max();
        Console.WriteLine($"[Day14/Part2]: Highest points: {highestPoints}");
    }

    private static bool CheckReindeerFlying(Dictionary<string, int> reindeer, int currentTime)
    {
        var cyclesTaken = currentTime / reindeer["cycleTime"]; 
        
        if (currentTime <= reindeer["travelTime"])
        {
            return true;
        }

        if (cyclesTaken > 0)
        {
            return (currentTime > cyclesTaken * reindeer["cycleTime"] &&
                    currentTime <= cyclesTaken * reindeer["cycleTime"] + reindeer["travelTime"]); 
        }
        
        return (currentTime >  reindeer["cycleTime"] &&
                currentTime <= reindeer["cycleTime"] + reindeer["travelTime"]);
    }

    private static Dictionary<string, Dictionary<string, int>> GetReindeerStats(string inputFile)
    {
        Dictionary<string, Dictionary<string, int>> reindeerStats = new ();
        using (var input = TextUtils.GetStreamReaderFromTextFile(inputFile))
        {
            while (!input.EndOfStream)
            {
                var line = input.ReadLine().TrimEnd('.').Split(' ');
                reindeerStats.Add(line[0], new Dictionary<string, int>()
                {
                    {"speed", int.Parse(line[3])},
                    {"travelTime", int.Parse(line[6])},
                    {"restTime", int.Parse(line[13])},
                    {"cycleTime", int.Parse(line[6]) + int.Parse(line[13])},
                    {"currentDistance", 0},
                    {"points", 0}
                });
            }
        }
        return reindeerStats;
    }
}