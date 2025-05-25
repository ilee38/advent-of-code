using CsharpDotNetUtils;

namespace AdventOfCode2015.Day6;

public class Day6
{
    private const string InputFilePath = @"Day6/input.txt";
    
    public static void PartOne()
    {
        var grid = new int[1000, 1000];
        var sr = TextUtils.GetStreamReaderFromTextFile(InputFilePath);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var coords = TextUtils.GetIntegersFromString(line);
            var (x1, y1) = (coords[0], coords[1]);
            var (x2, y2) = (coords[2], coords[3]);
            
            if (line.Contains("toggle"))
            {
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[x, y] = (grid[x, y] ^ 1);
                    }
                }
            }
            else if (line.Contains("on"))
            {
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[x, y] = 1;
                    }
                }
            }
            else if (line.Contains("off"))
            {
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[x, y] = 0;
                    }
                }
            }
        }
        
        // Count "on" lights
        var count = 0;
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                count += grid[x, y];
            }
        }
        Console.WriteLine($"[Day 6 / Part 1] Total lights lit {count}");
    }

    public static void PartTwo()
    {
        var grid = new int[1000, 1000];
        var sr = TextUtils.GetStreamReaderFromTextFile(InputFilePath);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var coords = TextUtils.GetIntegersFromString(line);
            var (x1, y1) = (coords[0], coords[1]);
            var (x2, y2) = (coords[2], coords[3]);
            
            if (line.Contains("toggle"))
            {
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[x, y] += 2;
                    }
                }
            }
            else if (line.Contains("on"))
            {
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[x, y] += 1;
                    }
                }
            }
            else if (line.Contains("off"))
            {
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[x, y] = grid[x, y] > 0 ? grid[x, y] -= 1 : 0;
                    }
                }
            }
        }
        
        // Count brightness 
        var count = 0;
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                count += grid[x, y];
            }
        }
        Console.WriteLine($"[Day 6 / Part 2] Total brightness: {count}");    
    }
}