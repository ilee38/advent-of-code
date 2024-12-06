using CsharpDotNetUtils;

namespace AdventOfCode2024.Day5;

public class Day5
{
    private Dictionary<int, HashSet<int>> _orderingRules = new();
    private double _runningTotal = 0;

    public void PartOneMiddleNumber()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day5/day5-input.txt");
        while (true)
        {
            var line = streamReader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                break;

            // Create rules dictionary
            var pages = TextUtils.GetIntegersFromString(line);
            if (_orderingRules.ContainsKey(pages[0]))
            {
                _orderingRules[pages[0]].Add(pages[1]);
            }
            else
            {
                _orderingRules.Add(pages[0], new HashSet<int>(pages[1]));
            }
        }

        while (!streamReader.EndOfStream)
        {
            // Start checking updates lists
            var line = streamReader.ReadLine();
            var update = TextUtils.GetIntegersFromString(line);
            if (IsValidUpdate(update))
                _runningTotal += update[(update.Count - 1) / 2];
        }
        Console.WriteLine($"[Day5/Part1] Total middle pages: {_runningTotal}");
    }

    public void PartTwoMiddleNumberAfterOrdering()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day5/day5-input.txt");
        while (true)
        {
            var line = streamReader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                break;

            // Create rules dictionary
            var pages = TextUtils.GetIntegersFromString(line);
            if (_orderingRules.ContainsKey(pages[0]))
            {
                _orderingRules[pages[0]].Add(pages[1]);
            }
            else
            {
                _orderingRules.Add(pages[0], new HashSet<int>(pages[1]));
            }
        }

        while (!streamReader.EndOfStream)
        {
            // Start checking updates lists
            var line = streamReader.ReadLine();
            var update = TextUtils.GetIntegersFromString(line);
            if (!IsValidUpdate(update))
            {
                OrderUpdate(update);
                _runningTotal += update[(update.Count - 1) / 2];
            }
        }
        Console.WriteLine($"[Day5/Part2] Total middle pages: {_runningTotal}");
    }

    private bool IsValidUpdate(List<int> update)
    {
        var previousPagesWithNoRule = new HashSet<int>();

        for (var i = 0; i < update.Count - 1; i++)
        {
            var currentPage = update[i];
            for (var j = i + 1; j < update.Count; j++)
            {
                var nextPage = update[j];
                if (!_orderingRules.ContainsKey(currentPage) || !_orderingRules[currentPage].Contains(nextPage))
                {
                    previousPagesWithNoRule.Add(currentPage);
                }
                else if (_orderingRules[currentPage].Contains(nextPage) && previousPagesWithNoRule.Contains(nextPage))
                {
                    return false;
                }
            }

            foreach (var previousPage in previousPagesWithNoRule)
            {
                if (_orderingRules[currentPage].Contains(previousPage))
                {
                    return false;
                }
            }
        }

        return true;
    }

    // TODO: fix ordering of the update, currently not working.
    //  see this reddit post for ideas: https://www.reddit.com/r/adventofcode/comments/1h7mm3w/2024_day_05_part_2_how_nice_is_the_input_a_binary/
    private void OrderUpdate(List<int> update)
    {
        for (var i = update.Count - 1; i > 0; i--)
        {
            var currentPage = update[i];
            var currentPageIndex = i;
            for (var j = i - 1; j >= 0; j--)
            {
                var previousPage = update[j];
                if (_orderingRules[currentPage].Contains(previousPage))
                {
                    // swap pages
                    (update[currentPageIndex], update[j]) = (update[j], update[currentPageIndex]);
                    currentPageIndex = j;
                }
            }
        }
    }
}
