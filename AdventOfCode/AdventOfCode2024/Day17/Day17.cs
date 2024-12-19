using System;

namespace AdventOfCode2024.Day17;

public class Day17
{
    private int registerA = 30899381;
    private int registerB = 0;
    private int registerC = 0;
        
    // Instruction pointer
    private int IP = 0;
    private List<int> program = new(){2,4,1,1,7,5,4,0,0,3,1,6,5,5,3,0};

    public void PartOneProgramOutput()
    {
        List<int> output = new();

        while (true)
        {
            if (IP >= program.Count)
                break;

            var opCode = program[IP];
            var operand = program[IP + 1];
            var comboOperandValue = EvaluateComboOperand(operand);

            switch (opCode)
            {
                case 0:
                    registerA = (int)(registerA / Math.Pow(2, comboOperandValue));
                    IP += 2;
                    break;
                case 1:
                    registerB = registerB ^ operand;
                    IP += 2;
                    break;
                case 2:
                    registerB = comboOperandValue % 8;
                    IP += 2;
                    break;
                case 3:
                    if (registerA != 0)
                    {
                        IP = operand;
                    }
                    else
                    {
                        IP += 2;
                    }
                    break;
                case 4:
                    registerB = registerB ^ registerC;
                    IP += 2;
                    break;
                case 5:
                    var outValue = comboOperandValue % 8;
                    output.Add(outValue);
                    IP += 2;
                    break;
                case 6:
                    registerB = (int)(registerA / Math.Pow(2, comboOperandValue));
                    IP += 2;
                    break;
                case 7:
                    registerC = (int)(registerA / Math.Pow(2, comboOperandValue));
                    IP += 2;
                    break;
            }
        }
        Console.WriteLine("[Day 17 / Part 1] program output:");
        output.ForEach(n => Console.Write($"{n},"));
        Console.WriteLine("\n");
    }

    private int EvaluateComboOperand(int operand)
    {
        if (operand == 4)
            return registerA;
        if (operand == 5)
            return registerB;
        if (operand == 6)
            return registerC;

        return operand;
    }
}
