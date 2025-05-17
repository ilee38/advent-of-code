using CsharpDotNetUtils;

namespace AdventOfCode2015.Day2;

public class Day2
{
    private static string filePath = @"Day2/d2-input.txt";
    
    public void PartOneTotalFootage()
    {
        double totalFootage = 0;
        var sr = TextUtils.GetStreamReaderFromTextFile(filePath);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var dimensions = TextUtils.GetIntegersFromString(line);
            var length = dimensions[0];
            var width = dimensions[1];
            var height = dimensions[2];
            
            var side1Area = length * width;
            var side2Area = width * height;
            var side3Area = height * length;

            var smallestSide = Math.Min(side1Area, Math.Min(side2Area, side3Area));
            totalFootage += smallestSide + (2 * side1Area) + (2 * side2Area) + (2 * side3Area);
        }
        Console.WriteLine($"[Day 2 / Part 2]: total square footage: {totalFootage}");
    }

    public static void PartTwoTotalLength()
    {
        double totalRibbonLength = 0;
        var sr = TextUtils.GetStreamReaderFromTextFile(filePath);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var dimensions = TextUtils.GetIntegersFromString(line);
            var length = dimensions[0];
            var width = dimensions[1];
            var height = dimensions[2];
            
            var perimeter1 = (2 * length) + (2 * width);
            var perimeter2 = (2 * width) + (2 * height);
            var perimeter3 = (2 * length) + (2 * height);
            var smallestPerimeter = Math.Min(perimeter1, Math.Min(perimeter2, perimeter3));
            
            totalRibbonLength += smallestPerimeter + (length * width * height);
        }
        Console.WriteLine($"[Day 2 / Part 2]: total ribbon: {totalRibbonLength}");
    }
}