using CsharpDotNetUtils;

namespace AdventOfCode2025.Day3;

public class Day3
{
    public static void Part1()
    {
        var firstLargestNumber = 0;
        var firstLargestNumberIndex = 0;
        var secondLargestNumber = 0;
        var secondLargestNumberIndex = 0;
        var totalJoltage = 0;

        using (var batteryBanks = TextUtils.GetStreamReaderFromTextFile(@"Day3/input.txt"))
        {
            while (!batteryBanks.EndOfStream)
            {
                var batteryBank = batteryBanks.ReadLine();
                for (var i = 0; i < batteryBank.Length; i++)
                {
                    if (int.Parse(batteryBank[i].ToString()) > firstLargestNumber)
                    {
                        firstLargestNumber = int.Parse(batteryBank[i].ToString());
                        firstLargestNumberIndex = i;
                    }
                }

                for (var j = 0; j < batteryBank.Length; j++)
                {
                    if (j == firstLargestNumberIndex)
                    {
                        continue;
                    }

                    if (int.Parse(batteryBank[j].ToString()) > secondLargestNumber)
                    {
                        secondLargestNumber = int.Parse(batteryBank[j].ToString());
                        secondLargestNumberIndex = j;
                    }
                }

                var maxJoltage = string.Empty;
                if (firstLargestNumberIndex > secondLargestNumberIndex &&
                    firstLargestNumberIndex != batteryBank.Length - 1)
                {
                    secondLargestNumber = 0;
                    for (var k = firstLargestNumberIndex + 1; k < batteryBank.Length; k++)
                    {
                        if (int.Parse(batteryBank[k].ToString()) > secondLargestNumber)
                        {
                            secondLargestNumber = int.Parse(batteryBank[k].ToString());
                            secondLargestNumberIndex = k;
                        }
                    }
                }
                
                maxJoltage = firstLargestNumberIndex > secondLargestNumberIndex ? $"{secondLargestNumber}{firstLargestNumber}" : $"{firstLargestNumber}{secondLargestNumber}";
                
                totalJoltage += int.Parse(maxJoltage);
                
                firstLargestNumber = 0;
                firstLargestNumberIndex = 0;
                secondLargestNumber = 0;
                secondLargestNumberIndex = 0;
            }
        }
        
        Console.WriteLine($"[Day3 | Part1]: Total Joltage is {totalJoltage}");
    }
}