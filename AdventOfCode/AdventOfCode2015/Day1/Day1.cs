using CsharpDotNetUtils;

namespace AdventOfCode2015.Day1;

public class Day1
{
    private static string filePath = @"Day1/d1-input.txt";
    private int currentFloor = 0;

    public void PartOneFinalFloor()
    {
        var sr = TextUtils.GetStreamReaderFromTextFile(filePath);
        while (!sr.EndOfStream)
        {
            var parenthesis = sr.Read();
            switch (parenthesis)
            {
                case '(':
                    currentFloor++;
                    break;
                case ')':
                    currentFloor--;
                    break;
            }
        }
        
        Console.WriteLine($"[Day 1 / Part 1] Final floor: {currentFloor}");
    }

    public void PartTwoBasement()
    {
        currentFloor = 0;
        var currentPosition = 0;
        
        var sr = TextUtils.GetStreamReaderFromTextFile(filePath);
        while (!sr.EndOfStream)
        {
            var parenthesis = sr.Read();
            switch (parenthesis)
            {
                case '(':
                    currentFloor++;
                    break;
                case ')':
                    currentFloor--;
                    break;
            }
            
            currentPosition++;

            if (currentFloor == -1)
            {
                Console.WriteLine($"[Day 1 / Part 2] Position: {currentPosition}");
                break;
            }
        }
    }
}