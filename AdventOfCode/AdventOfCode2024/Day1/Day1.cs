namespace AdventOfCode2024.Day1;

public class Day1
{
    private List<double> leftList = new();
    private List<double> rightList = new();
    private Dictionary<double, int> rightColumnCounts = new();
    private double runningTotal = 0;
    private double similarityScore = 0;

    public void PartOneRunningTotal()
    {
        using var streamReader = new StreamReader(@"Day1/day-1-input.txt");
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            var iDs = line.Split("   ");
            leftList.Add(double.Parse(iDs[0]));
            rightList.Add(double.Parse(iDs[1]));
        }
        leftList.Sort();
        rightList.Sort();

        for (var i = 0; i < leftList.Count; i++)
        {
            runningTotal += Math.Abs(leftList[i] - rightList[i]);
        }
        Console.WriteLine($"Day1/Part1 total: {runningTotal}");
    }

    public void PartTwoSimilarityScore()
    {
        using var streamReader = new StreamReader(@"Day1/day-1-input.txt");
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            var iDs = line.Split("   ");
            leftList.Add(double.Parse(iDs[0]));
            var rightListId = double.Parse(iDs[1]);
            if (!rightColumnCounts.Keys.Contains(rightListId))
            {
                rightColumnCounts.Add(rightListId, 1);
            }
            else
            {
                rightColumnCounts[rightListId] += 1;
            }
        }

        foreach (var id in leftList)
        {
            if (rightColumnCounts.Keys.Contains(id))
            {
                similarityScore += (id * rightColumnCounts[id]);
            }
        }
        Console.WriteLine($"Day1/Part2 similarity score: {similarityScore}");
    }
}
