using System;
using System.Runtime.CompilerServices;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day25;

public class Day25
{
    private readonly List<int[]> _locks = new ();
    private readonly List<int[]> _keys = new ();

    private int uniquePairsCount = 0;

    public void PartOneCountUniquePairs()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day25/d25input.txt");

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            if (!string.IsNullOrEmpty(line))
            {
                if (line == "#####")
                {
                    // Process lock
                    int[] lockHeight = [0,0,0,0,0];
                    for (var i = 0; i < 5; i++)
                    {
                        var row = streamReader.ReadLine();
                        for (var j = 0; j < row.Length; j++)
                        {
                            if (row[j] == '#')
                            {
                                lockHeight[j] += 1;
                            }
                        }
                    }
                    _locks.Add(lockHeight);
                    var endRow = streamReader.ReadLine();
                }
                else if (line == ".....")
                {
                    // Process key
                    int[] keyHeight = [0,0,0,0,0];
                    for (var i = 0; i < 5; i++)
                    {
                        var row = streamReader.ReadLine();
                        for (var j = 0; j < row.Length; j++)
                        {
                            if (row[j] == '#')
                            {
                                keyHeight[j] += 1;
                            }
                        }
                    }
                    _keys.Add(keyHeight);
                    var endRow = streamReader.ReadLine();
                }
            }
        }

        TryKeys();
        Console.WriteLine($"[Day 25/Part 1] Unique key/lock combinations: {uniquePairsCount}");
    }

    private void TryKeys()
    {
        bool overlap = false;

        foreach (var _lock in _locks)
        {
            foreach (var key in _keys)
            {
                for (var i = 0; i < 5; i++)
                {
                    if (key[i] + _lock[i] > 5)
                    {
                        overlap = true;
                        break;
                    }
                }

                if (!overlap)
                {
                    uniquePairsCount++;
                }

                overlap = false;
            }
        }
    }
}
