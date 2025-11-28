using System.ComponentModel.Design;
using CsharpDotNetUtils;

namespace AdventOfCode2015.Day16;

public class Day16
{
    private static readonly Dictionary<string, int> ScannedCompounds = new Dictionary<string, int>()
    {
        { "children", 3},
        { "cats", 7 },
        { "samoyeds", 2},
        { "pomeranians", 3},
        { "akitas", 0},
        { "vizslas", 0},
        { "goldfish", 5},
        { "trees", 3},
        { "cars", 2},
        { "perfumes", 1}
    };
    
    public static void PartOne()
    {
        var auntSueNumber = string.Empty;
        using (var auntsInfo = TextUtils.GetStreamReaderFromTextFile(@"Day16/input.txt"))
        {
            while (!auntsInfo.EndOfStream)
            {
                var line = auntsInfo.ReadLine().Split(' ');
                auntSueNumber = line[1].TrimEnd(':');
                var compound1 = line[2].TrimEnd(':');
                var compound1Qty = int.Parse(line[3].TrimEnd(','));
                var compound2 = line[4].TrimEnd(':');
                var compound2Qty = int.Parse(line[5].TrimEnd(','));
                var compound3 = line[6].TrimEnd(':');
                var compound3Qty = int.Parse(line[7].TrimEnd(','));

                if (ScannedCompounds[compound1] == compound1Qty && ScannedCompounds[compound2] == compound2Qty &&
                    ScannedCompounds[compound3] == compound3Qty)
                {
                    break;
                }
            }
        }
        Console.WriteLine($"Day 16, part 1: {auntSueNumber}");
    }

    public static void PartTwo()
    {
        var auntSueNumber = string.Empty;
        using (var auntsInfo = TextUtils.GetStreamReaderFromTextFile(@"Day16/input.txt"))
        {
            while (!auntsInfo.EndOfStream)
            {
                var line = auntsInfo.ReadLine().Split(' ');
                auntSueNumber = line[1].TrimEnd(':');
                var compound1 = line[2].TrimEnd(':');
                var compound1Qty = int.Parse(line[3].TrimEnd(','));
                var compound2 = line[4].TrimEnd(':');
                var compound2Qty = int.Parse(line[5].TrimEnd(','));
                var compound3 = line[6].TrimEnd(':');
                var compound3Qty = int.Parse(line[7].TrimEnd(','));

                var compound1Match = CompoundMatches(compound1, compound1Qty);
                var compound2Match = CompoundMatches(compound2, compound2Qty);
                var compound3Match = CompoundMatches(compound3, compound3Qty);
                
                if (compound1Match && compound2Match && compound3Match)
                {
                    break;
                }
            }
        }
        Console.WriteLine($"Day 16, part 2: {auntSueNumber}");
    }

    private static bool CompoundMatches(string compound, int compoundQty)
    {
        if (compound.Equals("cats", StringComparison.InvariantCultureIgnoreCase) || compound.Equals("trees", StringComparison.InvariantCultureIgnoreCase))
        {
            return compoundQty > ScannedCompounds[compound];
        }
        
        if (compound.Equals("pomeranians", StringComparison.InvariantCultureIgnoreCase) || compound.Equals("goldfish", StringComparison.InvariantCultureIgnoreCase))
        {
            return compoundQty < ScannedCompounds[compound];
        }
        
        return ScannedCompounds[compound] == compoundQty;
    }
}