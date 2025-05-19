using CsharpDotNetUtils;

namespace AdventOfCode2015.Day3;

public class Day3
{
    private const string InputFile = @"Day3/d3-input.txt";
    
    public static void PartOneAtLeastOnePresent()
    {
        var coordinatesSet = new HashSet<Tuple<int, int>>();
        var currentCoordinates = new Tuple<int, int>(0, 0);
        var sr = TextUtils.GetStreamReaderFromTextFile(InputFile);
        
        coordinatesSet.Add(currentCoordinates);
        
        while (!sr.EndOfStream)
        {
            var direction = sr.Read();
            switch (direction)
            {
                case '^':
                    currentCoordinates = new Tuple<int, int>(currentCoordinates.Item1, currentCoordinates.Item2 + 1);
                    coordinatesSet.Add(currentCoordinates);
                    break;
                case 'v':
                    currentCoordinates = new Tuple<int, int>(currentCoordinates.Item1, currentCoordinates.Item2 - 1);
                    coordinatesSet.Add(currentCoordinates);
                    break;
                case '>':
                    currentCoordinates = new Tuple<int, int>(currentCoordinates.Item1 + 1, currentCoordinates.Item2);
                    coordinatesSet.Add(currentCoordinates);
                    break;
                case '<':
                    currentCoordinates = new Tuple<int, int>(currentCoordinates.Item1 - 1, currentCoordinates.Item2);
                    coordinatesSet.Add(currentCoordinates);
                    break;
            }
        }
        Console.WriteLine($"[Day 3 / Part 1] At least one present: {coordinatesSet.Count}");
    }

    public static void PartTwoAtLeastOnePresent()
    {
        var coordinatesSet = new HashSet<Tuple<int, int>>();
        var santaCurrentCoordinates = new Tuple<int, int>(0, 0);
        var roboSantaCurrentCoordinates = new Tuple<int, int>(0, 0);
        var santaTurn = true;
        var sr =  TextUtils.GetStreamReaderFromTextFile(InputFile);
        
        coordinatesSet.Add(santaCurrentCoordinates);    // Initially both coordinates at the same, so we just add one of them to the set

        while (!sr.EndOfStream)
        {
            var direction = sr.Read();
            switch (direction)
            {
                case '^':
                    if (santaTurn)
                    {
                        santaCurrentCoordinates = new Tuple<int, int>(santaCurrentCoordinates.Item1, santaCurrentCoordinates.Item2 + 1);
                        santaTurn = false;
                        coordinatesSet.Add(santaCurrentCoordinates);
                    }
                    else
                    {
                        roboSantaCurrentCoordinates = new Tuple<int, int>(roboSantaCurrentCoordinates.Item1, roboSantaCurrentCoordinates.Item2 + 1);
                        santaTurn = true;
                        coordinatesSet.Add(roboSantaCurrentCoordinates);
                    }
                    break;
                case 'v':
                    if (santaTurn)
                    {
                        santaCurrentCoordinates = new Tuple<int, int>(santaCurrentCoordinates.Item1, santaCurrentCoordinates.Item2 - 1);
                        santaTurn = false;
                        coordinatesSet.Add(santaCurrentCoordinates);
                    }
                    else
                    {
                        roboSantaCurrentCoordinates = new Tuple<int, int>(roboSantaCurrentCoordinates.Item1, roboSantaCurrentCoordinates.Item2 - 1);
                        santaTurn = true;
                        coordinatesSet.Add(roboSantaCurrentCoordinates);
                    }
                    break;
                case '>':
                    if (santaTurn)
                    {
                        santaCurrentCoordinates = new Tuple<int, int>(santaCurrentCoordinates.Item1 + 1, santaCurrentCoordinates.Item2);
                        santaTurn = false;
                        coordinatesSet.Add(santaCurrentCoordinates);
                    }
                    else
                    {
                        roboSantaCurrentCoordinates = new Tuple<int, int>(roboSantaCurrentCoordinates.Item1 + 1, roboSantaCurrentCoordinates.Item2);
                        santaTurn = true;
                        coordinatesSet.Add(roboSantaCurrentCoordinates);
                    }
                    break;
                case '<':
                    if (santaTurn)
                    {
                        santaCurrentCoordinates = new Tuple<int, int>(santaCurrentCoordinates.Item1 - 1, santaCurrentCoordinates.Item2);
                        santaTurn = false;
                        coordinatesSet.Add(santaCurrentCoordinates);
                    }
                    else
                    {
                        roboSantaCurrentCoordinates = new Tuple<int, int>(roboSantaCurrentCoordinates.Item1 - 1, roboSantaCurrentCoordinates.Item2);
                        santaTurn = true;
                        coordinatesSet.Add(roboSantaCurrentCoordinates);
                    }
                    break;
            }
        }
        Console.WriteLine($"[Day 3 / Part 2] At least one present: {coordinatesSet.Count}");
    }
}