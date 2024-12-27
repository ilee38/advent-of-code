using System;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day24;

public class Day24
{
    private readonly Dictionary<string, int> _inputWires = new ();
    private readonly int[] _zOutputs = new int[46];
    private readonly Queue<string[]> _pendingGates = new ();
    private int _zOutputCounter = 0;

    public void PartOneGatesOutput()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day24/d24input.txt");

        // Process all x and y inputs
        while (true)
        {
            var line = streamReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            var inputWire = line.Split(": ");
            _inputWires.Add(inputWire[0], int.Parse(inputWire[1]));
        }

        // Process logic gates
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            var gateOperation = line.Split(" ");

            var logicOp = gateOperation[1];
            var inputA = gateOperation[0];
            var inputB = gateOperation[2];
            var outputName = gateOperation[4];

            if (!_inputWires.ContainsKey(inputA) || !_inputWires.ContainsKey(inputB))
            {
                _pendingGates.Enqueue(gateOperation);
            }
            else
            {
                PerformLogicOperation(logicOp, inputA, inputB, outputName);
            }
        }

        if (_zOutputCounter < _zOutputs.Length)
        {
            // Process pending gates
            while (_pendingGates.Count() > 0)
            {
                var gateOperation = _pendingGates.Dequeue();

                var logicOp = gateOperation[1];
                var inputA = gateOperation[0];
                var inputB = gateOperation[2];
                var outputName = gateOperation[4];

                if (!_inputWires.ContainsKey(inputA) || !_inputWires.ContainsKey(inputB))
                {
                    _pendingGates.Enqueue(gateOperation);
                }
                else
                {
                    PerformLogicOperation(logicOp, inputA, inputB, outputName);
                }
            }
        }

        var decimalOutput = GetDecimalFromBitsArray();
        Console.WriteLine($"[Day 24 / part 1] Decimal output: {decimalOutput}");
    }

    private void PerformLogicOperation(string logicOp, string inputA, string inputB, string outputName)
    {
        int outputValue;

        switch (logicOp)
        {
            case "AND":
                outputValue = _inputWires[inputA] & _inputWires[inputB];
                RecordOutput(outputName, outputValue);
                break;
            case "OR":
                outputValue = _inputWires[inputA] | _inputWires[inputB];
                RecordOutput(outputName, outputValue);
                break;
            case "XOR":
                outputValue = _inputWires[inputA] ^ _inputWires[inputB];
                RecordOutput(outputName, outputValue);
                break;
        }
    }

    private void RecordOutput(string outputName, int outputValue)
    {
        _inputWires.Add(outputName, outputValue);

        if (outputName.StartsWith('z'))
        {
            var index = TextUtils.GetIntegersFromString(outputName).First();
            _zOutputs[index] = outputValue;
            _zOutputCounter++;
        }
    }

    private double GetDecimalFromBitsArray()
    {
        double total = 0;
        
        for (var i = 0; i < _zOutputs.Length; i++)
        {
            total += _zOutputs[i] * Math.Pow(2, i);
        }

        return total;
    }
}
