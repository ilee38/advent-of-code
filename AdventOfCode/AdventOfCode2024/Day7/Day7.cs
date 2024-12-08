using System;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day7;

public class Day7
{
    private double _totalCalibrationResult = 0;

    public void PartOneTotalCalibrationSum()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day7/d7input.txt");
        while (!streamReader.EndOfStream)
        {
            var equation = TextUtils.GetDoublesFromString(streamReader.ReadLine());
            if (IsValidEquation(equation))
            {
                _totalCalibrationResult += equation[0];
            }
        }
        Console.WriteLine($"[Day 7/ Part 1] Total calibration result: {_totalCalibrationResult}");
    }

    private static bool IsValidEquation(List<double> equation)
    {
        var testValue = equation[0];
        
        return false;
    } 
}
