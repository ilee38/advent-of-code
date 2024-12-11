using System;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day11;

public class Day11
{
    private readonly Dictionary<double, List<double>> _stones = new();
    private readonly Dictionary<double, List<int>> _stoneCountPerBlink = new();
    private double _totalRemovedStones = 0;

    public void PartOneStones25Blinks()
    {
        var input = TextUtils.ReadAllTextToString(@"Day11/d11input.txt").Split(" ");

        // initialize dictionary to hold stones
        foreach (var stone in input)
        {
            var parsedStone = double.Parse(stone);
            _stones.Add(parsedStone, new List<double>{parsedStone});
            _stoneCountPerBlink.Add(parsedStone, new List<int>{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0});
        }

        Blink25Times();
        var totalStones = CountFinalStones();
        Console.WriteLine($"[Day 11 / Part 1] Total stones: {totalStones}");
    }

    private void Blink25Times()
    {
        for (var blink = 0; blink < 25; blink++)
        {
            foreach (var stoneIndex in _stones.Keys)
            {   
                var currentStoneListCount = _stones[stoneIndex].Count;
                var stonesPerBlink = _stoneCountPerBlink[stoneIndex][blink];
                for (var i = 0; i < stonesPerBlink; i++)
                {           
                    var currentStone = _stones[stoneIndex][currentStoneListCount - (stonesPerBlink - i)];
                    if (currentStone == 0)
                    {
                        _stones[stoneIndex].Add(1);
                        _stoneCountPerBlink[stoneIndex][blink + 1] += 1;
                        _totalRemovedStones++;               
                    }
                    else if (currentStone.ToString().Length % 2 == 0)
                    {
                        var half = currentStone.ToString().Length / 2;
                        _stones[stoneIndex].Add(double.Parse(currentStone.ToString().Substring(0, half)));
                        _stones[stoneIndex].Add(double.Parse(currentStone.ToString().Substring(half, half)));
                        _stoneCountPerBlink[stoneIndex][blink + 1] += 2;
                        _totalRemovedStones++;
                    }
                    else
                    {
                        _stones[stoneIndex].Add(currentStone * 2024);
                        _stoneCountPerBlink[stoneIndex][blink + 1] += 1;
                        _totalRemovedStones++;
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
        return runnigTotal - _totalRemovedStones;
    }

}
