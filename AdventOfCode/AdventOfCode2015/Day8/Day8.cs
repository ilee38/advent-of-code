using CsharpDotNetUtils;

namespace AdventOfCode2015.Day8;

public class Day8
{
    private const string FilePath = @"Day8/input.txt";
    
    public static void PartOne()
    {
        var sr = TextUtils.GetStreamReaderFromTextFile(FilePath);
        var charactersOfCode = 0;
        var charactersInMemory = 0;

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            charactersOfCode += line.Length;
            
            var characters = line.ToCharArray();
            
            // Skip first and last double quotes
            for (var i = 1; i < line.Length - 1; i++)
            {
                var character = characters[i];
                if (character == '\\')
                {
                    i++;
                    character  = characters[i];
                    if (character == '\\' || character == '"')
                    {
                        charactersInMemory += 1;
                    }
                    else if (character == 'x')
                    {
                        charactersInMemory += 1;
                        i += 2;
                    }
                }
                else
                {
                    charactersInMemory += 1;
                }
            }
        }
        Console.WriteLine($"[Day8/Part1]: total: {charactersOfCode - charactersInMemory}");
    }

    public static void PartTwo()
    {
        var sr = TextUtils.GetStreamReaderFromTextFile(FilePath);
        var charactersOfCode = 0;
        var encodedCharacters = 0;

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            charactersOfCode += line.Length;
            
            var characters = line.ToCharArray();
            // Add required additional characters: 2 double quotes + 2 backslashes
            encodedCharacters += 6;
            
            // Skip first and last double quotes
            for (var i = 1; i < line.Length - 1; i++)
            {
                var character = characters[i];
                if (character == '\\' || character == '"')
                {
                    encodedCharacters += 2;
                }
                else
                {
                    encodedCharacters += 1;
                }
            }
        }
        Console.WriteLine($"[Day8/Part2]: total: {encodedCharacters - charactersOfCode}");
    }
}