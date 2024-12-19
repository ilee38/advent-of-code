using System;
using CsharpDotNetUtils;
using Microsoft.VisualBasic;

namespace AdventOfCode2024.Day19;

public class Day19
{
    private HashSet<string> _towelPatterns;
    private int _possibleDesigns = 0;
    
    private static readonly int MAX_PATTERN_LENGTH = 8;

    public void PartOnePossibleTowelDesigns()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day19/d19input.txt");
        var patterns = streamReader.ReadLine().Split(", ");
        _towelPatterns = patterns.ToHashSet();
        
        var emptyLine = streamReader.ReadLine();
        while (!streamReader.EndOfStream)
        {
            var design = streamReader.ReadLine();
            if (DesignIsPossible(design))
            {
                _possibleDesigns++;
            }
        }
        Console.WriteLine($"[Day 19 / Part 1] Possible designs: {_possibleDesigns}");
    }

    // TODO: current answer is too low. Need to verify correctness of the method.
    private bool DesignIsPossible(string design)
    {
        var startIndex = 0;
        var currentLength = MAX_PATTERN_LENGTH;

        while (startIndex < design.Length)
        {
            if (startIndex + currentLength > design.Length)
            {
                currentLength = design.Length - startIndex;
            }

            var pattern = design.Substring(startIndex, currentLength);
            if (_towelPatterns.Contains(pattern))
            {
                startIndex += currentLength;
                currentLength = MAX_PATTERN_LENGTH;
            }
            else
            {
                currentLength--;
            }

            if (currentLength == 0)
                return false;
        }
        return true;
    }
}
