using CsharpDotNetUtils;

namespace AdventOfCode2024.Day4;

public class Day4
{
    private List<char[]> letterSoup = new();
    private int runningTotal = 0;

    public void PartOneXmasSearch()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day4/day4-input.txt");
        while (!streamReader.EndOfStream)
        {
            letterSoup.Add(streamReader.ReadLine().ToCharArray());
        }

        for (var i = 0; i < letterSoup.Count; i++)
        {
            for (var j = 0; j < letterSoup[i].Length; j++)
            {
                if (letterSoup[i][j] == 'X')
                {
                    runningTotal += SearchHorizontal(i, j);
                    runningTotal += SearchVertical(i, j);
                    runningTotal += SearchPositiveDiagonal(i, j);
                    runningTotal += SearchNegativeDiagonal(i, j);
                }
            }
        }
        Console.WriteLine($"Day 4 / part 1. XMAS word count: {runningTotal}");
    }

    public void PartTwoXslashMasSearch()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day4/day4-input.txt");
        while (!streamReader.EndOfStream)
        {
            letterSoup.Add(streamReader.ReadLine().ToCharArray());
        }

        for (var i = 0; i < letterSoup.Count; i++)
        {
            for (var j = 0; j < letterSoup[i].Length; j++)
            {
                if (letterSoup[i][j] == 'A')
                {
                    if (SearchPositiveDiagonalX(i, j) && SearchNegativeDiagonalX(i, j))
                        runningTotal++;
                }
            }
        }
        Console.WriteLine($"Day 4 / part 2. X-MAS word count: {runningTotal}");
    }

    private int SearchHorizontal(int i, int j)
    {
        var count = 0;
        if (j >= 3)
        {
            // look left
            if (letterSoup[i][j - 1] == 'M' && letterSoup[i][j - 2] == 'A' && letterSoup[i][j - 3] == 'S')
                count++;
        }

        if (j < letterSoup[i].Length - 3)
        {
            // look right
            if (letterSoup[i][j + 1] == 'M' && letterSoup[i][j + 2] == 'A' && letterSoup[i][j + 3] == 'S')
                count++;
        }

        return count;
    }

    private int SearchVertical(int i, int j)
    {
        var count = 0;
        if (i >= 3)
        {
            // look up
            if (letterSoup[i - 1][j] == 'M' && letterSoup[i - 2][j] == 'A' && letterSoup[i - 3][j] == 'S')
                count++;
        }

        if (i < letterSoup.Count - 3)
        {
            // look down
            if (letterSoup[i + 1][j] == 'M' && letterSoup[i + 2][j] == 'A' && letterSoup[i + 3][j] == 'S')
                count++;
        }

        return count;
    }

    private int SearchPositiveDiagonal(int i, int j)
    {
        var count = 0;
        if (i >= 3 && j < letterSoup[i].Length - 3)
        {
            // look up
            if (letterSoup[i - 1][j + 1] == 'M' && letterSoup[i - 2][j + 2] == 'A' && letterSoup[i - 3][j + 3] == 'S')
                count++;

        }

        if (i < letterSoup.Count - 3 && j >= 3)
        {
            // look down
            if (letterSoup[i + 1][j - 1] == 'M' && letterSoup[i + 2][j - 2] == 'A' && letterSoup[i + 3][j - 3] == 'S')
                count++;
        }

        return count;
    }

    private int SearchNegativeDiagonal(int i, int j)
    {
        var count = 0;
        if (i >= 3 && j >= 3)
        {
            // look up
            if (letterSoup[i - 1][j - 1] == 'M' && letterSoup[i - 2][j - 2] == 'A' && letterSoup[i - 3][j - 3] == 'S')
                count++;
        }

        if (i < letterSoup.Count - 3 && j < letterSoup[i].Length - 3)
        {
            // look down
            if (letterSoup[i + 1][j + 1] == 'M' && letterSoup[i + 2][j + 2] == 'A' && letterSoup[i + 3][j + 3] == 'S')
                count++;
        }

        return count;
    }

    private bool SearchPositiveDiagonalX(int i, int j)
    {
        var found = false;
        if ((i >= 1 && i < letterSoup.Count - 1) && (j >= 1 && j < letterSoup[i].Length - 1))
        {
            // look up then down for either "MAS" or "SAM"
            if (letterSoup[i - 1][j + 1] == 'M' && letterSoup[i + 1][j - 1] == 'S')
                found = true;

            if (letterSoup[i - 1][j + 1] == 'S' && letterSoup[i + 1][j - 1] == 'M')
                found = true;
        }

        return found;
    }

    private bool SearchNegativeDiagonalX(int i, int j)
    {
        var found = false;
        if ((i >= 1 && i < letterSoup.Count - 1 ) && (j >= 1 && j < letterSoup[i].Length - 1))
        {
            // look up then down for either "MAS" or "SAM"
            if (letterSoup[i - 1][j - 1] == 'M' && letterSoup[i + 1][j + 1] == 'S')
                found = true;

            if (letterSoup[i - 1][j - 1] == 'S' && letterSoup[i + 1][j + 1] == 'M')
                found = true;
        }

        return found;
    }
}
