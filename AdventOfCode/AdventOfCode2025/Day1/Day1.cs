using CsharpDotNetUtils;

namespace AdventOfCode2025.Day1;

public class Day1
{
    public static void PartOne()
    {
        var zerosCount = 0;
        var currentPosition = 50;

        using (var rotations = TextUtils.GetStreamReaderFromTextFile(@"Day1/input.txt"))
        {
            while (!rotations.EndOfStream)
            {
                var line = rotations.ReadLine();
                var direction = line.Substring(0, 1);
                var distance = int.Parse(line.Substring(1));

                switch (direction)
                {
                    case "L":
                        currentPosition = (currentPosition - distance) % 100;
                        break;
                    case "R":
                        currentPosition = (currentPosition + distance) % 100;
                        break;
                    default:
                        break;
                }

                if (currentPosition == 0)
                {
                    zerosCount++;
                }
            }
        }
        Console.WriteLine($"Day 1, part 1: password: {zerosCount}");
    }
}