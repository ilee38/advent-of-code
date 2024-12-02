namespace AdventOfCode2024.Day2;

public class Day2
{
    private int runnigTotalSafeReports = 0;

    public void PartOneSafeReports()
    {
        using var streamReader = new StreamReader(@"Day2/day2-input.txt");
        while (!streamReader.EndOfStream)
        {
            var report = streamReader.ReadLine();
            var levels = report.Split(" ");
            List<int> parsedLevels = new();
            foreach (var level in levels)
            {
                parsedLevels.Add(int.Parse(level));
            }

            if (AscendingAndDiffInRange(parsedLevels) || DescendingAndDiffInRange(parsedLevels))
            {
                runnigTotalSafeReports += 1;
            }
        }
        Console.WriteLine($"Day2/part1 - total safe reports: {runnigTotalSafeReports}");
    }

    private static bool AscendingAndDiffInRange(List<int> parsedLevels)
    {
        for (var i = 0; i < parsedLevels.Count - 1; i++)
        {
            if (parsedLevels[i] >= parsedLevels[i + 1])
            {
                return false;
            }

            if (parsedLevels[i + 1] - parsedLevels[i] < 1 || parsedLevels[i + 1] - parsedLevels[i] > 3)
            {
                return false;
            }
        }

        return true;
    }

    private static bool DescendingAndDiffInRange(List<int> parsedLevels)
    {
        for (var i = 0; i < parsedLevels.Count - 1; i++)
        {
            if (parsedLevels[i] <= parsedLevels[i + 1])
            {
                return false;
            }

            if (parsedLevels[i] - parsedLevels[i + 1] < 1 || parsedLevels[i] - parsedLevels[i + 1] > 3)
            {
                return false;
            }
        }

        return true;
    }
}
