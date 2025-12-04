using System.Text.RegularExpressions;
using CsharpDotNetUtils;

namespace AdventOfCode2025.Day2;

public class Day2
{
    public static void Part1()
    {
        long invalidIdCount = 0;
        using (var idRanges = TextUtils.GetStreamReaderFromTextFile(@"Day2/input.txt"))
        {
            var ranges = idRanges.ReadLine().Split(",");
            foreach (var range in ranges)
            {
                var line = range.Split("-");
                var min = long.Parse(line[0]);
                var max = long.Parse(line[1]);

                for (var i = min; i <= max; i++)
                {
                    if (InvalidIdPartOne(i))
                    {
                        invalidIdCount += i;
                    }
                }
            }
        }
        Console.WriteLine($"[Day2|Part1]: {invalidIdCount}");
    }

    public static void Part2()
    {
        long invalidIdCount = 0;
        using (var idRanges = TextUtils.GetStreamReaderFromTextFile(@"Day2/input.txt"))
        {
            var ranges = idRanges.ReadLine().Split(",");
            foreach (var range in ranges)
            {
                var line = range.Split("-");
                var min = long.Parse(line[0]);
                var max = long.Parse(line[1]);

                for (var i = min; i <= max; i++)
                {
                    if (InvalidIdPartTwo(i))
                    {
                        invalidIdCount += i;
                    }
                }
            }
        }
        Console.WriteLine($"[Day2|Part2]: {invalidIdCount}");
    }

    private static bool InvalidIdPartOne(long id)
    {
        var stringId = id.ToString();
        var leftHalf = stringId.Substring(0, stringId.Length / 2);
        var rightHalf = stringId.Substring(stringId.Length / 2);
        
        return string.Equals(leftHalf, rightHalf);
    }

    private static bool InvalidIdPartTwo(long id)
    {
        var stringId = id.ToString();
        for (var length = 1; length <= stringId.Length / 2; length++)
        {
            if (stringId.Length % length == 0)
            {
                string pattern = $@"^(.{{{length}}})\1+$";
                if (Regex.IsMatch(stringId, pattern))
                {
                    return true;
                }
            }
        }
        return false;
    }
}