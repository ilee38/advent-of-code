using CsharpDotNetUtils;

namespace AdventOfCode2015.Day7;

public class Day7
{
    private const string InputFilePath = @"Day7/input.txt";

    public static void PartOneAndTwo()
    {
        // initialize wires b and c
        // parse the input file to get the values of all wires (expressions) save them to table
        // start solving for wire a (use a stack)
        var circuit = new Dictionary<string, string[]>();
        var solvedWires = new Dictionary<string, int>()
        {
            { "b", 46065 }, // Part 1 was = 1674 },
            { "c", 0 }
        };
        
        var sr = TextUtils.GetStreamReaderFromTextFile(InputFilePath);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var parts = line.Split(' ');
            var wire = parts[^1];
            string[] bitwiseOperation;
            
            if (line.Contains("NOT"))
            {
                bitwiseOperation = [parts[0], parts[1]];
            }
            else if (line.Contains("AND") || line.Contains("OR") || line.Contains("LSHIFT") || line.Contains("RSHIFT"))
            {
                bitwiseOperation = [parts[0], parts[1], parts[2]];
            }
            else
            {
                bitwiseOperation = [parts[0]];
            }

            circuit.TryAdd(wire, bitwiseOperation);
        }
        // Run circuit starting from wire a going back (DFS)
        RunCircuit(circuit, "a", solvedWires);
        Console.WriteLine($"[Day 7/Part 1]: Signal in a = {solvedWires["a"]}");
    }

    private static void RunCircuit(Dictionary<string, string[]> circuit, string wire, Dictionary<string, int> solvedWires)
    {
        string[] signals = [];
        switch (circuit[wire].Length)
        {
            case 1:
                if (int.TryParse(circuit[wire][0], out var number))
                {
                    solvedWires.TryAdd(wire, number);     
                }
                else
                {
                    signals = [circuit[wire][0]];
                } 
                break;
            case 2:
                signals = [circuit[wire][1]];
                break;
            case 3:
                signals = [circuit[wire][0], circuit[wire][2]];
                break;
        }

        foreach (var signal in signals)
        {
            // Check if one of the signals is a number (for LSHIFT and RSHIFT operations)
            if (int.TryParse(signal, out var number))
            {
                continue; 
            }
            if (!solvedWires.ContainsKey(signal))
            {
                RunCircuit(circuit, signal, solvedWires);
            }
        }

        var result = PerformOperation(circuit, wire, solvedWires);
        solvedWires.TryAdd(wire, result);
    }

    private static int PerformOperation(Dictionary<string, string[]> circuit, string wire, Dictionary<string, int> solvedWires)
    {
        var result = 0;
        var expression = circuit[wire];
       
        // Check if the expression is only a single signal, e.g. lx -> a
        if (expression.Length == 1)
        {
            return solvedWires[expression[0]];
        }
        
        var bitwiseOperator = expression.Length == 2 ? expression[0] : expression[1];
        switch (bitwiseOperator)
        {
            case "NOT":
                result = ~(solvedWires[expression[1]]);
                break;
            case "AND":
                // The first operand in an AND operation can be a number, e.g. 1 AND el -> em
                if (int.TryParse(expression[0], out var number))
                {
                    result = number & solvedWires[expression[2]];
                }
                else
                {
                    result = solvedWires[expression[0]] & solvedWires[expression[2]];     
                }
                break;
            case "OR":
                result = solvedWires[expression[0]] | solvedWires[expression[2]];
                break;
            case "LSHIFT":
                result = solvedWires[expression[0]] << int.Parse(expression[2]);
                break;
            case "RSHIFT":
                result = solvedWires[expression[0]] >> int.Parse(expression[2]);
                break;
        }
        return result;
    }
}