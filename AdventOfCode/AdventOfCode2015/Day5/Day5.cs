using System.Text.RegularExpressions;
using CsharpDotNetUtils;

namespace AdventOfCode2015.Day5;

public class Day5
{
    private const string InputFile = @"Day5/d5-input.txt";
    
    public void PartOneNaughtyOrNice()
    {
        var stringCount = 0;
        var sr = TextUtils.GetStreamReaderFromTextFile(InputFile);
        
        var repeatingLetters = new Regex(@"([a-z])\1",  RegexOptions.Compiled);
        var threeVowels = new Regex(@"(?:[aeiou].*){3,}", RegexOptions.Compiled);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine().ToLowerInvariant();
            
            if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
            {
                continue;
            }

            if (repeatingLetters.IsMatch(line) && threeVowels.IsMatch(line))
            {
                stringCount++;
            }
        }
        Console.WriteLine($"[Day 5 / Part One]: Nice strings: {stringCount}"); 
    }

    public void PartTwoNaughtyOrNice()
    {
        var stringCount = 0;
        var sr = TextUtils.GetStreamReaderFromTextFile(InputFile);
        
        var repeatingPair = new Regex(@"([a-z]{2}).*\1", RegexOptions.Compiled);
        var letterBetweenPair = new Regex(@"([a-z]).\1", RegexOptions.Compiled);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine().ToLowerInvariant();
            if (repeatingPair.IsMatch(line) && letterBetweenPair.IsMatch(line))
            {
                stringCount++;
            }
        }
        Console.WriteLine($"[Day 5 / Part Two]: Nice strings: {stringCount}");
    }
}