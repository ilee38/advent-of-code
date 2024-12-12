using System;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day11;

public class Day11
{
    private readonly Queue<double> _stoneQueue = new();

    public void PartOneStones25Blinks()
    {
        var input = TextUtils.ReadAllTextToString(@"Day11/d11input.txt").Split(" ");

        // initialize queue to process stones
        foreach (var stone in input)
        {
             _stoneQueue.Enqueue(double.Parse(stone));
        }

        BlinkXTimes(25);
        Console.WriteLine($"[Day 11 / Part 1] Total stones: {_stoneQueue.Count}");
    }

    // TODO: implement efficient solution to part 2 (using caching)
    public void PartTwoStones75Blinks()
    {
        var input = TextUtils.ReadAllTextToString(@"Day11/d11input.txt").Split(" ");

        // initialize queue to process stones
        foreach (var stone in input)
        {
            _stoneQueue.Enqueue(double.Parse(stone));
        }

        BlinkXTimes(75);
        Console.WriteLine($"[Day 11 / Part 2] Total stones: {_stoneQueue.Count}");
    }

    private void BlinkXTimes(int blinks)
    {
        for (var blink = 0; blink < blinks; blink++)
        {
            var stonesInCurrentBlink = _stoneQueue.Count;
            while (stonesInCurrentBlink > 0)
            {             
                var currentStone = _stoneQueue.Dequeue();
                if (currentStone == 0)
                {
                    _stoneQueue.Enqueue(1);              
                }
                else if (currentStone.ToString().Length % 2 == 0)
                {
                    var half = currentStone.ToString().Length / 2;
                    _stoneQueue.Enqueue(double.Parse(currentStone.ToString().AsSpan(0, half)));
                    _stoneQueue.Enqueue(double.Parse(currentStone.ToString().AsSpan(half, half)));
                    
                }
                else
                {
                    _stoneQueue.Enqueue(currentStone * 2024);
                }
                stonesInCurrentBlink--;
            }
        }
    }
}
