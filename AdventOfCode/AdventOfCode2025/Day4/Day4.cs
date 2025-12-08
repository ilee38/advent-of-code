using CsharpDotNetUtils;

namespace AdventOfCode2025.Day4;

public class Day4
{
    public static void Part1()
    {
        var diagram = LoadDiagram(@"Day4/input.txt");
        var accessibleRolls = CountAccessibleRolls(diagram);
        
        Console.WriteLine($"[Day 4 | Part 1]: {accessibleRolls}");
    }

    
    public static void Part2()
    {
        var diagram = LoadDiagram(@"Day4/input.txt");
        var removableRollsCount = 0;
        
        while (true)
        {
            var removableRolls = CountAndRemoveAccessibleRolls(diagram);
            
            if (removableRolls == 0)
            {
                break;
            }
            removableRollsCount += removableRolls;
        }
        Console.WriteLine($"[Day 4 | Part 2]: {removableRollsCount}");
    }
    

    private static int CountAccessibleRolls(List<List<char>> diagram)
    {
        var length = diagram.Count;
        var width = diagram[0].Count;
        var accessibleRolls = 0;
        var adjacentRollCount = 0;
        
        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var position = diagram[i][j];
                if (position == '.')
                {
                    continue;
                }
                
                if (j + 1 < width) // check East
                {
                    adjacentRollCount = diagram[i][j + 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (j - 1 >= 0) // check West
                {
                    adjacentRollCount = diagram[i][j - 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i - 1 >= 0) // check North
                {
                    adjacentRollCount = diagram[i - 1][j]  == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i + 1 < length) // check South
                {
                    adjacentRollCount = diagram[i + 1][j] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i - 1 >= 0 && j - 1 >= 0) // check Northwest
                {
                    adjacentRollCount = diagram[i - 1][j - 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i - 1 >= 0 && j + 1 < width) //check Northeast
                {
                    adjacentRollCount = diagram[i - 1][j + 1]  == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i + 1 < length && j - 1 >= 0)   // check Southwest
                {
                    adjacentRollCount = diagram[i + 1][j - 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i + 1 < length && j + 1 < width) // check Southeast
                {
                    adjacentRollCount = diagram[i + 1][j + 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }
                
                accessibleRolls = adjacentRollCount < 4 ? accessibleRolls + 1 : accessibleRolls;
                adjacentRollCount = 0;
            }
        }
        return accessibleRolls;
    }

    private static int CountAndRemoveAccessibleRolls(List<List<char>> diagram)
    {
        var length = diagram.Count;
        var width = diagram[0].Count;
        var accessibleRolls = 0;
        var adjacentRollCount = 0;
        var rollsToRemove = new List<Tuple<int, int>>();
        
        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var position = diagram[i][j];
                if (position == '.' || position == 'x')
                {
                    continue;
                }
                
                if (j + 1 < width) // check East
                {
                    adjacentRollCount = diagram[i][j + 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (j - 1 >= 0) // check West
                {
                    adjacentRollCount = diagram[i][j - 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i - 1 >= 0) // check North
                {
                    adjacentRollCount = diagram[i - 1][j]  == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i + 1 < length) // check South
                {
                    adjacentRollCount = diagram[i + 1][j] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i - 1 >= 0 && j - 1 >= 0) // check Northwest
                {
                    adjacentRollCount = diagram[i - 1][j - 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i - 1 >= 0 && j + 1 < width) //check Northeast
                {
                    adjacentRollCount = diagram[i - 1][j + 1]  == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i + 1 < length && j - 1 >= 0)   // check Southwest
                {
                    adjacentRollCount = diagram[i + 1][j - 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (i + 1 < length && j + 1 < width) // check Southeast
                {
                    adjacentRollCount = diagram[i + 1][j + 1] == '@' ? adjacentRollCount + 1 : adjacentRollCount;
                }

                if (adjacentRollCount < 4)
                {
                    accessibleRolls += 1;
                    rollsToRemove.Add(new Tuple<int, int>(i, j));
                }
                adjacentRollCount = 0;
            }
        }
        
        RemoveRolls(diagram, rollsToRemove);
        return accessibleRolls;
    }


    private static void RemoveRolls(List<List<char>> diagram, List<Tuple<int, int>> rollsToRemove)
    {
        foreach (var roll in rollsToRemove)
        {
            diagram[roll.Item1][roll.Item2] = 'x';
        }
    }

    
    private static List<List<char>> LoadDiagram(string filename)
    {
        var diagram = new List<List<char>>();
        
        using (var input = TextUtils.GetStreamReaderFromTextFile(filename))
        {
            while (!input.EndOfStream)
            {
                var line = input.ReadLine();
                diagram.Add(line.ToCharArray().ToList());
            }
        }
        return diagram;
    }
}