using System;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day11;

public class Day11
{
    private readonly Dictionary<double, List<double>> _stones = new();
    private readonly Dictionary<double, List<int>> _stoneCountPerBlink = new();

    public void PartOneStones25Blinks()
    {
        var input = TextUtils.ReadAllTextToString(@"Day11/testinput.txt").Split(" ");

        // initialize dictionary to hold stones
        foreach (var stone in input)
        {
            var parsedStone = double.Parse(stone);
            _stones.Add(parsedStone, new List<double>{parsedStone});
            _stoneCountPerBlink.Add(parsedStone, new List<int>{1});
        }

        Blink25Times();
        var totalStones = CountFinalStones();
        Console.WriteLine($"[Day 11 / Part 1] Total stones: {totalStones}");
    }

    // TODO: debug blink function
    private void Blink25Times()
    {
        for (var blink = 0; blink < 25; blink++)
        {
            foreach (var stone in _stones.Keys)
            {
                var stonesPerBlink = _stoneCountPerBlink[stone][blink];
                for (var i = 0; i < stonesPerBlink; i++)
                {
                    var currentStoneList = _stones[stone];
                    var currentStone = currentStoneList[currentStoneList.Count - (stonesPerBlink - i)];
                    if (currentStone == 0)
                    {
                        _stones[stone].Add(1);
                        _stoneCountPerBlink[stone].Add(1);
                    }
                    else if (currentStone.ToString().Length % 2 == 0)
                    {
                        var half = currentStone.ToString().Length / 2;
                        _stones[stone].Add(int.Parse(currentStone.ToString().Substring(0, half)));
                        _stones[stone].Add(int.Parse(currentStone.ToString().Substring(half, half)));
                        _stoneCountPerBlink[stone].Add(2);
                    }
                    else
                    {
                        _stones[stone].Add(currentStone * 2024);
                        _stoneCountPerBlink[stone].Add(1);
                    }
                }
            }
        }
    }
    
    private Double CountFinalStones()
    {
        var runnigTotal = 0;
        foreach (var initialStone in _stones.Keys)
        {
            runnigTotal += _stones[initialStone].Count;
        }
        return runnigTotal;
    }

}
