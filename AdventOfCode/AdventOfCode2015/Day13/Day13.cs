using CsharpDotNetUtils;

namespace AdventOfCode2015.Day13;

public class Day13
{
    public void PartOneAndTwo()
    {
        Dictionary<string, Dictionary<string, int>> guests = GetGuestList();
        var guestArrangement = guests.Keys.ToList();
        var greatestHappinessScore = int.MinValue;
        
        // Generate permutations of the names list
        foreach (var arrangement in GetPermutations(guestArrangement))
        {
            var happinessScore = 0;
            string currentGuest;
            string nextGuest; 
            for (var i = 0; i < arrangement.Count - 1; i++)
            {
                currentGuest = arrangement[i];
                nextGuest = arrangement[i + 1];
                happinessScore += guests[currentGuest][nextGuest];
                happinessScore += guests[nextGuest][currentGuest];
            }
            
            // circle back in the table
            currentGuest = arrangement[arrangement.Count - 1];
            nextGuest = arrangement[0];
            happinessScore += guests[currentGuest][nextGuest];
            happinessScore += guests[nextGuest][currentGuest];

            if (happinessScore > greatestHappinessScore)
            {
                greatestHappinessScore = happinessScore;
            }
        }
        
        Console.WriteLine($"[Day13/Part1]: total change in happiness {greatestHappinessScore}");
    }

    /// <summary>
    /// For part one, use input.txt file, and for part two use input2.txt file
    /// </summary>
    /// <returns></returns>
    private static Dictionary<string, Dictionary<string, int>> GetGuestList()
    {
        var guests = new Dictionary<string, Dictionary<string, int>>();
        
        using (var input = TextUtils.GetStreamReaderFromTextFile(@"Day13/input2.txt"))
        {
            while (!input.EndOfStream)
            {
                var line = input.ReadLine().Split(' ');
                var guestName = line[0];
                var score = line[2].Equals("lose") ? -1 * int.Parse(line[3]) : int.Parse(line[3]);
                var neighbour = line[10].TrimEnd('.');

                if (!guests.ContainsKey(guestName))
                {
                    guests.Add(guestName, new Dictionary<string, int>());
                    guests[guestName].Add(neighbour, score);
                }
                else
                {
                    guests[guestName].Add(neighbour, score);
                }
            }
        }
        return guests;
    }

    private IEnumerable<List<string>> GetPermutations(List<string> guests)
    {
        if (guests.Count == 0)
        {
            yield return new List<string>();
            yield break;
        }

        for (var i = 0; i < guests.Count; i++)
        {
            var currentGuest = guests[i];
            var remainingGuests = guests.Take(i).Concat(guests.Skip(i + 1)).ToList();
            
            foreach (var permutation in GetPermutations(remainingGuests))
            {
                permutation.Insert(0, currentGuest);
                yield return permutation;
            }
        }
    }
}